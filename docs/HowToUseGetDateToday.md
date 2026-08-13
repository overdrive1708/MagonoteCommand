# 今日の日付を取得する
## 構文と使用例
```txt
使用法:
  MagonoteCommand.exe GetDateToday [options]

オプション:
  --calendarType <Gregorian|Japanese>  カレンダーの種類 [default: Gregorian]
  --dateFormat <dateFormat>            日付の書式 [default: yyyy/MM/dd]
  --pause                              終了時に何かのキーの押下を必要とする

使用例：
  MagonoteCommand.exe GetDateToday --calendarType Gregorian --dateFormat yyyy/MM/dd --pause
```

## オプションの詳細
### --calendarType
Gregorian：西暦を取得します｡  
Japanese：和暦を取得します｡  

### --dateFormat
--dateFormatはカスタム日時形式文字列となります｡  
詳細は[カスタム日時形式文字列 - .NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/standard/base-types/custom-date-and-time-format-strings)をご確認ください｡  

--dateFormatと--calendarTypeの組み合わせで取得できる値が変わります｡(2026/08/13の例)  

| --dateFormat | --calendarType Gregorian | --calendarType Japanese|
| --- | --- | --- |
| yyyy/MM/dd | 2026/08/13 | 08/08/13 |
| ggyyyy年MM月dd日 | 西暦2026年08月13日 | 令和08年08月13日 |

### --pause
--pauseを指定すると､終了時に何らかのキーの押下を必要とします｡  
