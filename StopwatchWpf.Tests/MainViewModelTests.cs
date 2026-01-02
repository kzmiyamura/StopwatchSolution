using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;
using Moq;
using StopwatchLogic;                  // クラスライブラリ
using StopwatchWpf.Settings;           // StopwatchSettings
using StopwatchWpf.ViewModels;         // MainViewModel

namespace StopwatchWpf.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        /// <summary>
        /// テスト用の MainViewModel を作る共通メソッド
        /// （DI の代わりをここでやる）
        /// </summary>
        private static MainViewModel CreateViewModel(Mock<IStopwatch> mockStopwatch)
        {
            var settings = Options.Create(new StopwatchSettings
            {
                UpdateIntervalMs = 100
            });

            return new MainViewModel(mockStopwatch.Object, settings);
        }

        [TestMethod]
        public void StartCommand_Execute_ShouldCallStart()
        {
            // Arrange（準備）
            var mockStopwatch = new Mock<IStopwatch>();
            var vm = CreateViewModel(mockStopwatch);

            // Act（実行：ボタン押下を再現）
            vm.StartCommand.Execute(null);

            // Assert（検証）
            mockStopwatch.Verify(s => s.Start(), Times.Once);
        }

        [TestMethod]
        public void StopCommand_Execute_ShouldCallStop()
        {
            // Arrange
            var mockStopwatch = new Mock<IStopwatch>();
            var vm = CreateViewModel(mockStopwatch);

            // Act
            vm.StopCommand.Execute(null);

            // Assert
            mockStopwatch.Verify(s => s.Stop(), Times.Once);
        }

        [TestMethod]
        public void ResetCommand_Execute_ShouldCallReset_AndResetElapsedText()
        {
            // Arrange
            var mockStopwatch = new Mock<IStopwatch>();
            var vm = CreateViewModel(mockStopwatch);

            // 表示を事前に変更（擬似的に経過した状態）
            typeof(MainViewModel)
                .GetProperty("ElapsedText")!
                .SetValue(vm, "12:34.5");

            // Act
            vm.ResetCommand.Execute(null);

            // Assert
            mockStopwatch.Verify(s => s.Reset(), Times.Once);
            Assert.AreEqual("00:00.0", vm.ElapsedText);
        }
    }
}
