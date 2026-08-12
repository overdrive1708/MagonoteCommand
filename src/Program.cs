using System;
using System.Threading.Tasks;

namespace MagonoteCommand
{
    internal class Program
    {
        /// <summary>
        /// メイン処理(エントリーポイント)
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        private static void Main(string[] args)
        {
            // 未処理の例外が発生したときの処理を登録する
            EntryExceptionHandler();
        }

        /// <summary>
        /// 例外ハンドラー登録処理
        /// </summary>
        private static void EntryExceptionHandler()
        {
            TaskScheduler.UnobservedTaskException += ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;
        }
    }
}
