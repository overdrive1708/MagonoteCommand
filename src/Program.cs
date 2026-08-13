using System;
using System.CommandLine;
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

            // オプションの設定(--pause)
            Option<bool> optionEnablePause = new("--pause")
            {
                Description = Resources.Strings.CommandOptionEnablePause,
                DefaultValueFactory = _ => false
            };

            // オプションの設定(--calendarType)
            Option<DateTimeUtility.CalendarType> optionCalendarType = new("--calendarType")
            {
                Description = Resources.Strings.OptionDescriptionCalendarType,
                DefaultValueFactory = _ => DateTimeUtility.CalendarType.Gregorian
            };
            optionCalendarType.Validators.Add(result =>
            {
                switch (result.GetValueOrDefault<DateTimeUtility.CalendarType>())
                {
                    case DateTimeUtility.CalendarType.Gregorian:
                    case DateTimeUtility.CalendarType.Japanese:
                        break;
                    default:
                        result.AddError(Resources.Strings.OptionErrorCalendarType);
                        break;
                }
            });

            // オプションの設定(--dateFormat)
            Option<string> optionDateFormat = new("--dateFormat")
            {
                Description = Resources.Strings.OptionDescriptionDateFormat,
                DefaultValueFactory = _ => "yyyy/MM/dd"
            };

            // オプションの設定(--offsetDays)
            Option<double> optionOffsetDays = new("--offsetDays")
            {
                Description = Resources.Strings.OptionDescriptionOffsetDays,
                DefaultValueFactory = _ => 0
            };

            // ルートコマンドの設定
            RootCommand rootCommand = new(Resources.Strings.CommandDescriptionRoot);
            rootCommand.Options.Add(optionEnablePause);
            rootCommand.SetAction(parseResult => RootCommand(parseResult.GetValue(optionEnablePause)));

            // サブコマンドの設定(GetDateToday)
            Command subCommandGetDateToday = new("GetDateToday", Resources.Strings.CommandDescriptionGetDateToday);
            subCommandGetDateToday.Options.Add(optionCalendarType);
            subCommandGetDateToday.Options.Add(optionDateFormat);
            subCommandGetDateToday.Options.Add(optionEnablePause);
            subCommandGetDateToday.SetAction(parseResult => SubCommandGetDateToday(parseResult.GetValue(optionEnablePause),
                                                                                   parseResult.GetValue(optionCalendarType),
                                                                                   parseResult.GetValue(optionDateFormat)));
            rootCommand.Subcommands.Add(subCommandGetDateToday);

            // サブコマンドの設定(GetDateOffsetDays)
            Command subCommandGetDateOffsetDays = new("GetDateOffsetDays", Resources.Strings.CommandDescriptionGetDateOffsetDays);
            subCommandGetDateOffsetDays.Options.Add(optionCalendarType);
            subCommandGetDateOffsetDays.Options.Add(optionDateFormat);
            subCommandGetDateOffsetDays.Options.Add(optionOffsetDays);
            subCommandGetDateOffsetDays.Options.Add(optionEnablePause);
            subCommandGetDateOffsetDays.SetAction(parseResult => SubCommandGetDateOffsetDays(parseResult.GetValue(optionEnablePause),
                                                                                             parseResult.GetValue(optionCalendarType),
                                                                                             parseResult.GetValue(optionDateFormat),
                                                                                             parseResult.GetValue(optionOffsetDays)));
            rootCommand.Subcommands.Add(subCommandGetDateOffsetDays);


            // コマンドライン引数を解析してコマンドを実行する
            _ = rootCommand.Parse(args).Invoke();

            // アプリケーションの終了待ち
            ConsoleWrapper.Pause();
        }

        /// <summary>
        /// 例外ハンドラー登録処理
        /// </summary>
        private static void EntryExceptionHandler()
        {
            TaskScheduler.UnobservedTaskException += ExceptionHandler.OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;
        }

        /// <summary>
        /// ルートコマンドの処理
        /// </summary>
        /// <param name="isEnablePause">ポーズ有効フラグ</param>
        private static void RootCommand(bool isEnablePause)
        {
            ConsoleWrapper.IsEnablePause = isEnablePause;

            ConsoleWrapper.WriteLine(Resources.Strings.MessagePleaseSpecifyCommand, ConsoleWrapper.Destination.StandardOutput);
        }

        /// <summary>
        /// サブコマンド GetDateToday の処理
        /// </summary>
        /// <param name="isEnablePause">ポーズ有効フラグ</param>
        /// <param name="calendarType">カレンダーの種類</param>
        /// <param name="dateFormat">日付の書式</param>
        private static void SubCommandGetDateToday(bool isEnablePause, DateTimeUtility.CalendarType calendarType, string dateFormat)
        {
            ConsoleWrapper.IsEnablePause = isEnablePause;

            string getResult = DateTimeUtility.GetDateToday(calendarType, dateFormat);
            
            ConsoleWrapper.WriteLine(getResult, ConsoleWrapper.Destination.StandardOutput);
        }

        /// <summary>
        /// サブコマンド GetDateToday の処理
        /// </summary>
        /// <param name="isEnablePause">ポーズ有効フラグ</param>
        /// <param name="calendarType">カレンダーの種類</param>
        /// <param name="dateFormat">日付の書式</param>
        /// <param name="offsetDays">オフセット日数</param>
        private static void SubCommandGetDateOffsetDays(bool isEnablePause, DateTimeUtility.CalendarType calendarType, string dateFormat, double offsetDays)
        {
            ConsoleWrapper.IsEnablePause = isEnablePause;

            string getResult = DateTimeUtility.GetDateOffsetDays(calendarType, dateFormat, offsetDays);

            ConsoleWrapper.WriteLine(getResult, ConsoleWrapper.Destination.StandardOutput);
        }
    }
}
