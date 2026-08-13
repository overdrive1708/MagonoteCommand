using System;
using System.IO;
using System.Threading.Tasks;

namespace MagonoteCommand
{
    /// <summary>
    /// 例外ハンドラークラス
    /// </summary>
    internal class ExceptionHandler
    {
        //--------------------------------------------------
        // 定数
        //--------------------------------------------------
        /// <summary>
        /// 例外情報記録ファイル
        /// </summary>
        private const string FatalErrorInformationPath = @"FatalErrorInformation.log";

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// UnobservedTaskExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception.InnerException != null)
            {
                HandleException(e.Exception.InnerException);
            }
        }

        /// <summary>
        /// UnhandledExceptionイベント発生時の処理
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException((Exception)e.ExceptionObject);

        /// <summary>
        /// 例外発生時の処理
        /// </summary>
        /// <param name="e">例外情報</param>
        private static void HandleException(Exception exception)
        {
            // 例外の詳細情報をファイルに出力する
            using (StreamWriter fatalErrorInformationFile = new(FatalErrorInformationPath, true, System.Text.Encoding.UTF8))
            {
                fatalErrorInformationFile.WriteLine($"=========={DateTime.Now}==========");
                fatalErrorInformationFile.WriteLine(exception.ToString());
                fatalErrorInformationFile.Close();
            }

            // 例外が発生したことをコンソールに出力する
            ConsoleWrapper.WriteLine(Resources.Strings.MessageImportantNotice, ConsoleWrapper.Destination.StandardError);
            ConsoleWrapper.WriteLine(Resources.Strings.MessageFatalError, ConsoleWrapper.Destination.StandardError);

            // アプリケーションの終了待ち
            ConsoleWrapper.Pause();

            // 終了する
            Environment.Exit(1);
        }
    }
}
