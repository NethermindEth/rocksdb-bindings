// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class AdaptiveCommitDelayControllerTests
{
    /// <summary>
    /// Builds a controller whose recommended delay is a plain function of the reported lag:
    /// <c>emaAlpha: 1</c> makes the EMA equal the newest cluster lag, and the trend and burst
    /// weights select which term of the score is under test.
    /// </summary>
    private static AdaptiveCommitDelayController Deterministic(
        int historySize = 8,
        double trendWeight = 0,
        double burstWeight = 0,
        int minDelayMs = 0,
        int maxDelayMs = 100_000)
        => new(
            replicaCount: 1,
            historySize: historySize,
            emaAlpha: 1.0,
            minDelayMs: minDelayMs,
            maxDelayMs: maxDelayMs,
            delayPerLagUnitMs: 1.0,
            lagUnit: 1.0,
            trendWeight: trendWeight,
            burstWeight: burstWeight);

    private static void Report(AdaptiveCommitDelayController controller, params long[] lags)
    {
        foreach (var lag in lags)
            controller.ReportLag(new ReplicaLagSample(0, lag));
    }

    [Test]
    [Arguments(0, 20, 0.25, 0, 2000, 5.0, 1.0, 2.0, 1.5, "replicaCount")]
    [Arguments(1, 0, 0.25, 0, 2000, 5.0, 1.0, 2.0, 1.5, "historySize")]
    [Arguments(1, 20, 0.0, 0, 2000, 5.0, 1.0, 2.0, 1.5, "emaAlpha")]
    [Arguments(1, 20, 1.5, 0, 2000, 5.0, 1.0, 2.0, 1.5, "emaAlpha")]
    [Arguments(1, 20, 0.25, -1, 2000, 5.0, 1.0, 2.0, 1.5, "minDelayMs")]
    [Arguments(1, 20, 0.25, 100, 99, 5.0, 1.0, 2.0, 1.5, "maxDelayMs")]
    [Arguments(1, 20, 0.25, 0, 2000, -1.0, 1.0, 2.0, 1.5, "delayPerLagUnitMs")]
    [Arguments(1, 20, 0.25, 0, 2000, 5.0, -1.0, 2.0, 1.5, "lagUnit")]
    [Arguments(1, 20, 0.25, 0, 2000, 5.0, 1.0, -1.0, 1.5, "trendWeight")]
    [Arguments(1, 20, 0.25, 0, 2000, 5.0, 1.0, 2.0, -1.0, "burstWeight")]
    public async Task Constructor_RejectsOutOfRangeArgument(
        int replicaCount,
        int historySize,
        double emaAlpha,
        int minDelayMs,
        int maxDelayMs,
        double delayPerLagUnitMs,
        double lagUnit,
        double trendWeight,
        double burstWeight,
        string expectedParameter)
    {
        var exception = await Assert.That(() => new AdaptiveCommitDelayController(
                replicaCount,
                historySize,
                emaAlpha,
                minDelayMs,
                maxDelayMs,
                delayPerLagUnitMs,
                lagUnit,
                trendWeight,
                burstWeight))
            .ThrowsExactly<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo(expectedParameter);
    }

    [Test]
    public async Task Constructor_AcceptsAnEmaAlphaOfOne()
        => await Assert.That(() => Deterministic()).ThrowsNothing();

    [Test]
    public async Task ReplicaLagSample_ClampsNegativeLagToZero()
        => await Assert.That(new ReplicaLagSample(3, -42).LagVersions).IsEqualTo(0);

    [Test]
    public async Task ReplicaLagSample_KeepsTheReplicaIndex()
        => await Assert.That(new ReplicaLagSample(3, 7).ReplicaIndex).IsEqualTo(3);

    [Test]
    public async Task ReportLag_NullSample_Throws()
    {
        var controller = Deterministic();
        await Assert.That(() => controller.ReportLag(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ReportLag_ReplicaIndexBeyondReplicaCount_Throws()
    {
        var controller = new AdaptiveCommitDelayController(replicaCount: 2);
        await Assert.That(() => controller.ReportLag(new ReplicaLagSample(2, 1)))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task GetRecommendedDelayMs_BeforeAnySample_IsTheMinimumDelay()
    {
        var controller = Deterministic(minDelayMs: 7);
        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(7);
    }

    [Test]
    public async Task GetRecommendedDelayMs_ScalesTheEmaByTheDelayPerLagUnit()
    {
        var controller = new AdaptiveCommitDelayController(
            replicaCount: 1,
            historySize: 4,
            emaAlpha: 1.0,
            minDelayMs: 0,
            maxDelayMs: 100_000,
            delayPerLagUnitMs: 3.0,
            lagUnit: 2.0,
            trendWeight: 0,
            burstWeight: 0);

        Report(controller, 10);

        // score 10 (ema only) * 3ms per 2 lag units
        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(15);
    }

    [Test]
    public async Task GetRecommendedDelayMs_AddsTheWeightedTrend()
    {
        var controller = Deterministic(historySize: 4, trendWeight: 3);

        Report(controller, 0, 10);

        // ema 10 + 3 * mean consecutive delta 10
        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(40);
    }

    [Test]
    public async Task GetRecommendedDelayMs_IgnoresADownwardTrend()
    {
        var controller = Deterministic(historySize: 4, trendWeight: 3);

        Report(controller, 10, 0);

        // ema 0, and the negative trend is floored at zero rather than credited
        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(0);
    }

    [Test]
    public async Task GetRecommendedDelayMs_AddsTheWeightedBurstAboveTheAverage()
    {
        var controller = Deterministic(historySize: 4, burstWeight: 2);

        Report(controller, 0, 0, 0, 8);

        // ema 8 + 2 * (current 8 - average 2)
        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(20);
    }

    [Test]
    public async Task GetRecommendedDelayMs_IsClampedToTheMaximum()
    {
        var controller = Deterministic(maxDelayMs: 50);

        Report(controller, 1_000_000);

        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(50);
    }

    [Test]
    public async Task GetRecommendedDelayMs_IsClampedToTheMinimum()
    {
        var controller = Deterministic(minDelayMs: 25);

        Report(controller, 0);

        await Assert.That(controller.GetRecommendedDelayMs()).IsEqualTo(25);
    }

    [Test]
    public async Task ClusterLag_IsTheMaximumAcrossReplicas()
    {
        var controller = new AdaptiveCommitDelayController(replicaCount: 3);

        controller.ReportLag(new ReplicaLagSample(0, 4));
        controller.ReportLag(new ReplicaLagSample(1, 11));
        controller.ReportLag(new ReplicaLagSample(2, 2));

        await Assert.That(controller.GetSnapshot().CurrentLag).IsEqualTo(11);
    }

    [Test]
    public async Task GetSnapshot_ReportsTheLatestLagOfEveryReplica()
    {
        var controller = new AdaptiveCommitDelayController(replicaCount: 3);

        controller.ReportLag(new ReplicaLagSample(0, 4));
        controller.ReportLag(new ReplicaLagSample(2, 9));
        controller.ReportLag(new ReplicaLagSample(0, 5));

        await Assert.That(controller.GetSnapshot().ReplicaLags).IsEquivalentTo(new long[] { 5, 0, 9 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetSnapshot_HistoryKeepsOnlyTheMostRecentWindow()
    {
        var controller = Deterministic(historySize: 3);

        Report(controller, 1, 2, 3, 4, 5);

        await Assert.That(controller.GetSnapshot().History).IsEquivalentTo(new long[] { 3, 4, 5 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetSnapshot_AverageLagIsTheMeanOfTheRetainedWindow()
    {
        var controller = Deterministic(historySize: 3);

        Report(controller, 1, 2, 3, 4, 5);

        await Assert.That(controller.GetSnapshot().AverageLag).IsEqualTo(4.0);
    }

    [Test]
    public async Task GetSnapshot_BeforeAnySample_IsAllZero()
    {
        var snapshot = Deterministic().GetSnapshot();

        using (Assert.Multiple())
        {
            await Assert.That(snapshot.CurrentLag).IsEqualTo(0);
            await Assert.That(snapshot.AverageLag).IsEqualTo(0.0);
            await Assert.That(snapshot.EmaLag).IsEqualTo(0.0);
            await Assert.That(snapshot.Trend).IsEqualTo(0.0);
            await Assert.That(snapshot.History).IsEmpty();
        }
    }

    [Test]
    public async Task EmaLag_IsSeededByTheFirstSampleRatherThanBlendedFromZero()
    {
        var controller = new AdaptiveCommitDelayController(replicaCount: 1, emaAlpha: 0.25);

        Report(controller, 100);

        await Assert.That(controller.GetSnapshot().EmaLag).IsEqualTo(100.0);
    }

    [Test]
    public async Task EmaLag_BlendsLaterSamplesByTheAlpha()
    {
        var controller = new AdaptiveCommitDelayController(replicaCount: 1, emaAlpha: 0.5);

        Report(controller, 10, 0);

        // 0.5 * 0 + 0.5 * 10
        await Assert.That(controller.GetSnapshot().EmaLag).IsEqualTo(5.0);
    }

    [Test]
    public async Task Trend_NeedsAtLeastTwoSamples()
    {
        var controller = Deterministic();

        Report(controller, 42);

        await Assert.That(controller.GetSnapshot().Trend).IsEqualTo(0.0);
    }

    [Test]
    public async Task Trend_IsTheMeanConsecutiveDelta()
    {
        var controller = Deterministic(historySize: 4);

        Report(controller, 0, 10, 20, 30);

        await Assert.That(controller.GetSnapshot().Trend).IsEqualTo(10.0);
    }

    [Test]
    public async Task DelayIfNeededAsync_WithNothingToWaitFor_CompletesSynchronously()
    {
        var controller = Deterministic();

        var task = controller.DelayIfNeededAsync();

        await Assert.That(task.IsCompletedSuccessfully).IsTrue();
    }

    /// <remarks>
    /// The delay is a full minute so that no scheduling stall between starting it and observing it
    /// could let it complete first, and it is cancelled rather than awaited so the test does not
    /// actually wait.
    /// </remarks>
    [Test]
    public async Task DelayIfNeededAsync_WaitsWhenALagWasReported()
    {
        var controller = Deterministic(minDelayMs: 60_000, maxDelayMs: 60_000);
        using var cancellation = new CancellationTokenSource();

        Report(controller, 1000);
        var task = controller.DelayIfNeededAsync(cancellation.Token);

        await Assert.That(task.IsCompleted).IsFalse();

        await cancellation.CancelAsync();
        await Assert.That(async () => await task).Throws<OperationCanceledException>();
    }

    [Test]
    public async Task DelayIfNeededAsync_ObservesCancellation()
    {
        var controller = Deterministic(minDelayMs: 10_000, maxDelayMs: 10_000);
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.That(async () => await controller.DelayIfNeededAsync(cancellation.Token))
            .Throws<OperationCanceledException>();
    }
}
