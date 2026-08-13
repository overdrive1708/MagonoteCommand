using System;

namespace MagonoteCommand
{
    /// <summary>
    /// Consoleクラスのラッパークラス
    /// </summary>
    internal class ConsoleWrapper
    {
        /// <summary>
        /// 出力先
        /// </summary>
        public enum Destination
        {
            StandardOutput,     // 標準出力
            StandardError       // 標準エラー出力
        }

        /// <summary>
        /// ポーズ有効フラグ
        /// </summary>
        public static bool IsEnablePause { get; set; } = false;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// Console.WriteLineラップ処理
        /// </summary>
        /// <param name="value">コンソールに出力する値</param>
        /// <param name="destination">出力先</param>
        internal static void WriteLine(string value, Destination destination)
        {
            switch (destination)
            {
                case Destination.StandardOutput:
                    Console.WriteLine(value);
                    break;
                case Destination.StandardError:
                    Console.Error.WriteLine(value);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ポーズ処理
        /// </summary>
        internal static void Pause()
        {
            if (IsEnablePause)
            {
                Console.WriteLine(Resources.Strings.MessagePleasePressAnyKey);
                _ = Console.ReadKey();
            }
        }
    }
}
