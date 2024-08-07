#Сравнение технологий для написания оконных приложений.

Как-то раз в чате прозвучала идея сравнить разные технологии для написания оконных приложений. В частности, под Windows. Используя средства языка C#. То есть, конечно, можно это сделать и с помощью C++, Python, Rust, JS и других языков, но мы же шарписты, нам интереснее именно то, что мы можем сами использовать, не меняя язык программирования.
Итак, оконные приложения на C#. Их можно написать, используя:
-	Windows Forms
-	WPF
-	WinUI
-	.NET MAUI
-	Avalonia
-	Uno Platform
Такое многообразие фреймворков обусловлено тем, что язык развивался, менялись подходы, совершенствовались технологии. А в какой-то момент к разработке подключились сторонние разработчики и к средствам Microsoft добавились Avalonia и Uno.

Таким образом, для того чтобы начать писать оконные приложения надо решить, используя какую технологию использовать. Но, кроме того, неплохо бы понимать, где будет приложение использоваться – только на Windows или есть необходимость в запуске на MacOS и Linux. F может быть еще и на IOS и Android?

В общем, в этой статье попробуем разобраться в возможностях этих фреймворков и сравним их. А сравнивать будем по таким критериям:
-	Платформы, в которых можно запустить приложение
-	Сложность разработки
-	Объем приложения

Для корректности сравнения будем использовать одну логику для всех приложений. Более того, попытаемся сделать их максимально похожими друг на друга. 

##Часть 1. Логика
Итак, чтобы не сравнивать пустые приложения без какой-либо функциональности, реализовал простейшую версию игры Крестики-Нолики. Логика самой игры простая: есть два игрока Х и О. Начинает игрок Х потом ходит О. Кто первым соберет три своих символа в ряд - выигрывает, если после последнего хода победителя нет, то ничья. 
Весь код логики практически один в один переходит из приложения в приложение, за исключением некоторых особенностей в разных фреймворках. Например, в Авалонии нет встроенного механизма отображения всплывающих окон (Message Box) и для того, чтобы он появился надо установить дополнительный пакет. Не сложно, но нюанс. В некоторых фреймворках нельзя напрямую посчитать кнопки из интерфейса и надо придумывать обходные пути.
Важно так же отметить, что везде, кроме WinForms по-хорошему следует использовать паттерн MVVM, однако для простейшего приложения заморачиваться не стал и работает все напрямую.

##Часть 2. Пользовательский интерфейс
Разработка UI в приложениях C# отличается не очень сильно друг от друга. Выделяется разве что WinForms, где элементы интерфейса размещаются обычно вручную в графическом редакторе, что в свою очередь весьма дружелюбно по отношению к людям без опыта. Еще можно размещать элементы вручную в WPF, однако рекомендуется все же выстраивать интерфейс вручную задавая параметры в коде страницы. 
Осложняет процесс разработки внешнего вида отсутствие интерактивного отображения получаемого результата в MAUI и WinUI – там посмотреть на результат можно только после сборки и запуска. Да, есть такая функция как Hot Reload, когда изменения будут появляться после горячей перезагрузки без перезапуска всего приложения, но тоже не без нюансов. Во-первых, при горячей перезагрузке теряется состояние приложения (если оно отдельно не сохраняется где-то), например при игре уже есть несколько крестиков и ноликов, то после Hot Reload они «забываются». Во-вторых, бывает, что изменения не отображаются и все равно приходится перезапускать приложение. Не баг, а фича, как говорится.
В целом же привыкнуть можно ко всему и даже потом возвращаться к WinForms становится тяжко – хочется все настраивать самому. 

##Часть 3. Платформы
Тут мы подходим к глобальным возможностям всех фреймворков. Итак, таблица ниже показывает, под какие платформы может быть собрано приложение.


|                     |     Windows    |     MacOS    |     Linux    |     Android    |     iOS    |
|---------------------|----------------|--------------|--------------|----------------|------------|
|     WinForms        |     +          |              |              |                |            |
|     WPF             |     +          |              |              |                |            |
|     WinUI           |     +          |              |              |                |            |
|     .NET MAUI       |     +          |     +        |              |     +          |     +      |
|     Avalonia        |     +          |     +        |     +        |     +          |     +      |
|     Uno Platform    |     +          |     +        |     +        |     +          |     +      |

Как видим, что вполне логично, все шесть фреймворков позволяют писать оконные приложения для Windows. Но есть и различия. Они кроются как в кроссплатформенности MAUI, Avalonia и Uno, так и в компонентах, которые используются для отрисовки графического интерфейса. Углубляться сильно не будем в дебри, но стоит знать, что MAUI Авалония используют компоненты WinUI, Uno в cвою очередь может реализовывать оконное приложение как в WPF варианте, так и WinUI. Это может быть полезно в отдельных случаях. Кроме того, нативные фреймворки WinForms, WPF и WinUI отличаются технически:
- Windows Forms использует GDI+ для отрисовки интерфейса
- WPF использует DirectX для рендеринга
- WinUI использует для рендеринга DirectX/DirectComposition

Кроме того, есть различие в используемых компонентах, библиотеках и подходах. Иными словами, все технологии, представленные в статье, имеют множество различий при схожей функциональности даже в рамках одной целевой платформы.

##Часть 4. Память
Сейчас мы подошли к той части «исследования», когда в дело вступают цифры. В данной части посмотрим на то, сколько приложения занимают места в оперативной памяти. Сравнивать будем в двух режимах: в Visual Studio в режиме Debug и в диспетчере задач запущенное опубликованное приложение.


|                     | WinForms   | WPF         | WinUI       | .NET MAUI   | Avalonia    | Uno Platform |
|---------------------|------------|-------------|-------------|-------------|-------------|--------------|
|     Debug (Mb)      |     15     |     80      |     80      |     132     |     82      |     62       |
|     Release (Mb)    |     6,1    |     19,6    |     26,2    |     54,4    |     32,6    |     13,6     |
 
Результаты интересные. Закономерно, что WinForms приложение самое легкое – задействует меньше всего ресурсов, как при отладке, так и при работе собранного приложения. А вот с остальными интереснее. Самое «тяжелое» приложение получилось на MAUI. Возможно, это результат кроссплатформенности – слишком много всего «внутри». Зато удивил Uno Platform – готовое приложение занимает меньше всех места, после WinForms и это отличный результат. 

Отдельно хотел замерить скорость запуска приложений, но в итоге делать этого не стал. Скажу только, что MAUI и тут проигрывает, явно уступая в скорости запуска всем остальным приложениям.


##Часть 5. Сборка и развертывание
Отдельно пару слов про то, насколько сложно подготовить проект к публикации и использовать его потом. При публикации приложений почти у всех можно получить файлы библиотек и запускающий файл, однако без установки запускаются только WinForms и WPF приложения, а так же, внезапно, Avalonia. Впрочем, это не такая уж проблема.. Была бы, если не одно Но: для того, чтобы поделиться приложением или его где-то разместить нужен сертификат. Тестовый вы можете создать на своем компьютере, а вот заверенный, чтобы Windows не ругалась и не считала приложение подозрительным, просто так не получить. То есть он стоит денег, а в текущих условиях еще и надо найти, где его приобрести. И да, он ограничен по времени. 
Конечно, что все нюансы и наверно есть каки-то пути решения вопроса, да и вопрос сам не первоочередной важности. Но знать о таких нюансах стоит.

##Часть 6. Итог
Что ж, стоит подвести итоги нашего небольшого исследования-сравнения. Для кого вообще оно делалось? Для тех, кто не занимался разработкой оконных приложений, но хотел бы понять, какие есть актуальные технологии. И вот эти технологии мы рассмотрели. Весьма поверхностно, конечно, но и задачи писать энциклопедию о том, как написать оконное приложение со всеми тонкостями, не стояло. А вот сделать вывод о том, с чего начать вкатываться в мир разработки приложений под Windows, а потом и других платформ, вполне можно. Мое видение, основанное на имевшемся ранее и полученном сейчас такое:
- Начинать стоит все-таки с базы – с Windows Forms. Да, технология не нова, даже можно ее считать устаревшей. Но она живая и функциональная. Многие приемы на ней можно отработать, да и собрать действительно сложно приложение с графическим интерфейсом вполне реально. Да чего там – так делали, делали много и долго.
- Вторым шагом стоит изучить WPF. Эта технология позволит развить понимание паттерна MVVM, проектирование интерфейса в XAML и откроет путь к более современным стекам. 
- Дальше путь открыт ко всему. Я был приятно удивлен возможностям Uno Platform, не ожидал. Кроме того, что можно написать приложение под любую актуальную ОС, в том числе и мобильную, так еще и весьма щадяще эта технология относится, как оказалось, к ресурсам. Не то, чтобы это сильно критично, но, когда проект большой и нужно бороться за производительность, приложение, потребляющее меньше ресурсов будет выглядеть предпочтительнее на мой взгляд. Еще из важного – возможность собрать дизайн приложения Uno в Figma и импортировать код интерфейса прямо в проект в виде XAML-кода! Такого нет ни у кого из других рассмотренных фреймворков.

