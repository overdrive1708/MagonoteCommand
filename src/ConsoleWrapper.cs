using System;

namespace MagonoteCommand
{
    /// <summary>
    /// Consoleクラスのラッパークラス
    /// </summary>
    internal class ConsoleWrapper
    {
        //--------------------------------------------------
        // プロパティ
        //--------------------------------------------------
        /// <summary>
        /// verboseオプション
        /// </summary>
        public static bool IsVerboseMode { get; set; } = false;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Console.WriteLineラップ処理
        /// </summary>
        /// <param name="value">コンソールに出力する値</param>
        internal static void WriteLine(string value)
        {
            // verboseオプションが指定されている場合のみコンソールに出力する
            if (IsVerboseMode)
            {
                Console.WriteLine(value);
            }
        }

        /// <summary>
        /// Console.WriteLine拡張処理(エラーメッセージ用)
        /// </summary>
        /// <param name="value">コンソールに出力する値</param>
        internal static void WriteErrorLine(string value)
        {
            // verboseオプションに関わらずコンソールに赤字で出力する
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Console.ReadKeyラップ処理
        /// </summary>
        /// <returns>ConsoleKeyInfo</returns>
        internal static ConsoleKeyInfo ReadKey()
        {
            // verboseオプションが指定されている場合のみコンソールから入力する
            return IsVerboseMode ? Console.ReadKey() : new ConsoleKeyInfo();
        }
    }
}
