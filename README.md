| [Game rules](#Game-rules) | [Features](#Features) | [TODO](#TODO) | [Архитектура](#Архитектура) |[Naming](#Naming) | [My codestyle](#My-codestyle) |
|---------------------------|-----------------------|---------------|-----------------------------|------------------|-------------------------------|

# merge2

## В коде точки входа: 
  1) Infrastructure: [_Project/Core/Infrastructure/Bootstrapper.cs](https://github.com/gggittt/merge2/blob/main/Assets/_Project/Core/Infrastructure/Bootstrapper.cs)
  2) Запуск логики игры на геймплей сцене: [_Project/Core/EntryPointGameplay.cs](https://github.com/gggittt/merge2/blob/main/Assets/_Project/Core/EntryPointGameplay.cs)

## Game rules
Merge items of same value together 

## Features

> Ячейки создаются в runtime. До начала игры в объекте GameData можно настроить размер поля

> Перемещение объектов по игровому полю.

> Объединение предметов по общему признаку

https://github.com/user-attachments/assets/e220a543-5dd1-4b10-b0a1-ccfd3eae2f98

> Кнопка для генерации случайного предмета в случайную ячейку поля

https://github.com/user-attachments/assets/bce402db-7daf-477a-9941-5ce88f1d1e6b

> Обработка переполнения поля

> Обработка переполнения цепочки предметов (последний предмет в цепочке не должен объединяться).

https://github.com/user-attachments/assets/32bcb42c-1c1c-42c6-bdce-d57590c6ab84




## Архитектура
- по идее запускать с Bootstrap scene, но заработает и сразу с Gameplay scene для теста только геймлей (т.к. Zenject инсталлеры ращбиты по контекстам).
- довольно объемная папка Shared.Extensions - большинство методов мои, но в проекте они не все нужны.
  - Вынесены отдельно из Core, чтобы можно было использовать методы расширения и для EditorTools и Tests. Pool отдельно для этой же цели, может понадобится в Editor папке.
- в _Project\Editor лежат полезные EditorTools.
  - Добавил кнопки в Toolbar для запуска сцен из любого места проекта. Bootstrap / Gameplay ![image](https://github.com/user-attachments/assets/85650340-553f-42b8-a55c-9b50b4606f7e)


## Использованные плагины
- Zenject
- DoTween
- TMP
- Odin

## Использованные паттерны
- FSN
- ObjectsPool
- Observer
- EntryPoint
- Factory
- Template method

## Todo
- уменьшить связанность, использовать интерфейсы.
- add .asmdef's
- центрировать камеру в runtime под размер поля. 
- вынести в конфиги настройки (из [SerializeField] монобехов):
  - размер поля
  - maxItemLevel
  - shapes amount
- вместо монобехов сделать POCO и CompositionRoot. Сейчас сильная связанность (в компонентах Item, через RequireComponent).
- Исправить систематическое нарушение Law of Demeter в связке cell-item, по типу `cell.HoldedItem.MergeLevel.Set`
- refactor Cell to
  - GridCell : DropZone
  - PassItemForQuestZone : DropZone
- refactor Item: заменить enum на наследование?
  - Sword : Item
  - Shoe : Item
  - ...

    
## My codestyle
Для фикса codestyle всего проекта под себя, в Rider:
  1. `Ctrl+e, Ctrl+c` (Reformat and Cleanup Code)
  2. Настроить свой стиль (Select Profile)
     - в том числе можно настроить сортировку членов типа. Что будет выше nested types, приватные поля, конструкторы, индексаторы и т.п.
     - - применится не при печатании, а при `Ctrl+e, Ctrl+c` (Reformat and Cleanup Code) или `Ctrl+Alt+Enter` (Reformat Code)
  3. Выбрать настроенный стиль. Есть преднастроенные (см слева Profile: Full Cleanup, Reformat, ...) ![image](https://github.com/user-attachments/assets/f99bbc90-8822-4ee2-aa44-6b89a5c74c78)
  4. Select Scope -> Whole Solution
  5. Run

- (Это описание стиля в текущем проекте. Подстроюсь под codestyle будущей команды, перезапишу свои привычки)
- Не делаю отступ для namespace. Будет проще перейти на фичу C# 10 "File scoped namespaces", когда нет отступа для types.  
  - ![image](https://github.com/user-attachments/assets/e503d46c-a8ec-4009-8409-00aec11e9a11)
  - Настраивается в Rider: убрать галочку здесь:
  - ![image](https://github.com/user-attachments/assets/23592106-de71-4c76-bc93-52338fa2f64d)
  - Возможно стоит добавить _Project в "no namespace provider". А может и нет, т.к. тогда появляется вероятность что мои namespaces Будут конфликтовать с namespaces внешних плагинов
- Не пишу очевидный private
- prefer Type over var. Т.к. при чтении кода в начале строки глаза сразу ищут тип.
- внутри круглых скобок пишу пробелы. Мне так чуть лучше читается код. Без пробелов как будто бы "(" похожа на что-то среднее между "C" и _l_. 
  настраивается здесь:
  - ![image](https://github.com/user-attachments/assets/f30c82c2-1481-474f-85e8-2c03fae9bd2c)

<!--
- consts - PascalCase, even for private and local. Не вижу смысла в SNAKE_CASE, это просто замедляет печать. ![image](https://github.com/user-attachments/assets/4da2d2ed-2648-4eae-aeed-97b34347e98c)
 - [Inject] в поля, не только в Construct() 

- в остальном +- стандартно.
  - Возможно позже перейду с ```ctor(int some){ _some = some; }``` to ```ctor(int some){ this.some = some; }```
 
-->



