# ` для  нотации`

> Данный  не просит вас бежать и все . Это просто контент который нужно  и .

> И да, у меня . Я очень плохо вижу   и , которые , когда думал о чем то другом. Понять, .

В своей работе над  v1.0 я решил  идею, которая  еще на . А именно    для  и решения  нотации (для ЛЛ: Да, ).

Так вышло что на  я писал решение от начала до конца, в виде очень длинной  из switch - case, поэтому мне  не так уж и много: Удалить ее,  и   . А да! И  автомат!

# Задача

Итак, давайте  задачу:
> Мы (я), хотим  , в которой стэк  будет   , в виде  списка, На этапе *View* или **.   строку   стэка. Решать ,  в виде строки  нотации. А так же   для  и поиска по стэку  .

Теперь давайте . Я буду писать сразу  ,  дни  .

Для начала нам нужен **.

Таким образом мы создаем  функции, которую будем  внутри нашей  :

`typedef int (*) (float* items, float *result);`

`items` -  на первый  в массиве, с которым нужно .<br/>
`result` - , в который мы пишем  .<br/>
И  мы статус ошибки.

На момент  статьи  у нас  так (не   офертой):<br/>
`typedef struct calc_ {`<br/>
  *Полное имя для стороны клиента*<br/>
  `char *full_name;`<br/>
  *Символ для  нотации*<br/>
  `char *sym;`<br/>
  *  . 1 - унарный, 2 - *<br/>
  `int item_count;`<br/>
  * *<br/>
  `int ;`<br/>
  * символ (обычно )*<br/>
  `char *between_sym;`<br/>
  *Между  или числом,  символ*<br/>
  `bool op__digit;`<br/>
  *.  статус 0 - Ок*<br/>
  ` execut;`<br/>
  * на  элемент в стэке*<br/>
  `struct calc_ *next;`<br/>
`} c_;`<br/>

Ну и функции для  :

c_ `*init_`(char \*full_name, char \*sym, int , int item_count, bool between_digit, char \*between_sym,  op_func);

int `polish_solve`(const char \*str, const c_ \*stack, float *result);<br/>
int `polish_`(char \*str, c_ \*stack, char \*result);<br/>

int `find_`(const char \*str, const c_ \*stack, c_ \*\*result, bool by_sym);<br/>
int `free_c_`(c_ \*op);<br/>
int `push_`(c_ \*\*stack, c_ \*item);<br/>
c_ `*pop_`(c_ \*\*stack);<br/>

# `Простой `

Ко   мы  позже, на моменте разбора . А сейчас давайте  на фронт и  наш первый стэк, для  . , ,  и деление. Добавим и унарный минус.

 стэк  для них:<br/>
  c_ \*stack = `init_`("(", "(", 0, 0, true, "\*", NULL);<br/>
  `push_`(&stack, `init_`(")", ")", -1, 0, false, NULL, NULL));<br/>
  `push_`(&stack, `init_`("-", "-", 1, 2, false, "~", `minus`));<br/>
  `push_`(&stack, `init_`("+", "+", 1, 2, false, "#", `plus`));<br/>
  `push_`(&stack, `init_`("\*", "\*", 2, 2, false, NULL, `mult`));<br/>
  `push_`(&stack, `init_`("/", "/", 2, 2, false, NULL, ``));<br/>
  `push_`(&stack, `init_`("sin", "s", 5, 1, true, "*", sinus));<br/>
  

Я опишу только 3 функции , так как минус и   от , только знаком между . Скобки же не   функций:

int `plus`(float \*items, float \*result) {<br/>
&nbsp;int status = (items == NULL);<br/>
&nbsp;if (status == 0) {<br/>
&nbsp;&nbsp;\*result = items[0] + items[1];<br/>
&nbsp;}<br/>
&nbsp;return status;<br/>
}<br/>

int ``(float \*items, float\* result){<br/>
&nbsp;int status = (items == NULL);<br/>
&nbsp;&nbsp;if (status == 0) {<br/>
&nbsp;&nbsp;&nbsp;status = (items[1] == 0);<br/>
&nbsp;&nbsp;&nbsp;if (status == 0) {<br/>
&nbsp;&nbsp;&nbsp;&nbsp;\*result = items[0] / items[1];<br/>
&nbsp;&nbsp;&nbsp;}<br/>
&nbsp;}<br/>
&nbsp;return status;<br/>
}<br/>

int `negate`(float \*items, float \*result) {<br/>
&nbsp;int status = (items == NULL);<br/>
&nbsp;if (status == 0) {<br/>
&nbsp;&nbsp;\*result = -1 \* items[0];<br/>
&nbsp;}<br/>
&nbsp;return status;<br/>
}<br/>

Почему фронт  стэк, а  не зашиты в ?
Во-первых, только фронт  шрифт и символы,  будет  . Завтра мы все  на эмодзи... & 👆  🧮  🔜  🔄  🎃

Во-вторых, бэк понятия не имеет, какие статусы ошибок   и с каким кодом, а так он может задать себе те ошибки, какие захочет.

В-третьих, бэку вообще не важно что хочет собрать : Счеты,  или MathCad. Пускай там сам думает.  же может  , или создать что-то сложное внутри функции, и ввести это под каким ему угодным .  это унарный   rad в dec виде<br/>
`push_`(&stack, `init_`("rd", "rd", 2, 2, false, NULL, 'rad_to_dec'));

int `rad_to_dec`(float \*items, float \*result) {<br/>
&nbsp;int status = (items == NULL);<br/>
&nbsp;if (status == 0) {<br/>
&nbsp;&nbsp;\*result = items[0] \* 180/π;<br/>
&nbsp;}<br/>
&nbsp;return status;<br/>
}<br/>

Или ? ?   дня, для   в третьем доме?

Итак, фрон подает строку и стэк в .

float result = 0;<br/>
char \* = "-2 sin -3"<br/>
char \*polish = (char \*)`calloc`(strlen(str), sizeof(char));<br/>
`polish_`(, op_stack, polish);<br/>
`polish_solve`(polish, op_stack, &result);<br/>

Готово. Фронт  свои 300кк/сек и может идти дмой. Вы !

# `Бэк`

## `polish_(char *str, c_ *stack, char *result)`

Внутри данной функции, у нас будет стэк   и цикл по  строке. А так же флаг `last_digit = false`, на то что   число было цифрой. Внутри, путем if else цикл  на две секции:

if (`is_digit`(\*str) == 1)<br/>
else if (\*str != ' ')<br/>

То есть мы или  число, или, если это не пробел, мы  что перед нами .

### `is_digit`

Вцелом вы можете сами выбрать как  число.
Я это делаю через sscanf. Если мне удается его считать, то я  число  в char \*result. Этот способ  мне  с научной .

Если, после  числа, наверху стэка  унарный  (который мы считали до этого), то его  достать и вписать в  строку после числа.

То есть `-2 * ...` в момент  числа 2 уже будет иметь `~` (унарный минус) в стэке, и что бы  его к форме `2 ~ * ...`, мы должны его достать.

  сейчас задаст вопрос: "А нафига?!"  же и так потом  при  вводе `+`. И будет прав.  ему  в  нотацию запись типа `sin -x` (cо  любой дурак cможет).

### `is_`

Находим из строки .
`read_`(str, stack, &finded_op);

Если в  момент  условия вставки "между"-символа ( )<br/>
`last_digit` == `finded_op->op__digit` <br/>
и таковой вообще есть в <br/>
`finded_op->between_sym != NULL`, <br/>
то  его в стэк . 

Если `finded_op->op__digit == false`, то мы имеем дело с унарным  типа `-/+`, а значит мы  `finded_op`, если условие .
Если нет, то  `finded_op` в общий стэк и  оттуда все что выше или равно по .

Когда мы дойдем до конца строки, то запишем в нее символы всех  там . 

## `Пример`
  -2 Sin -3 на уровне бэка, что бы понять на сколько это все  и   в вид `op1` `dg1` `op2` `op3` `dg2`;

| `op1` | `dg1` | `op2` | `op3` | `dg2` |
| ------ | ------ | ------ | ------ | ------ |
| - | 2 | sin | - | 3 |
| "-"<br/>"-"<br/>1<br/>2<br/>false<br/>"~" | - | "sin"<br/>"s"<br/>1<br/>5<br/>true<br/>"\*" | "-"<br/>"-"<br/>1<br/>2<br/>false<br/>"~" | - |

>`bop` - between (между) operator

  `op1`. И так как  флаг last_digit == false и флаг `op1.between_digit == false`, находим "между"- `bop1` и  его вместо `op1`.<br/>
 2 и  из стэка унарный . 
Строка  вид `2 bop1`<br/>
 `op2`.<br/>
Перед `op2` мы считали число, о чем нам говорит флаг. Так же у него задан `bop2`.  `bop2` и `op2` в стэк
 из `op3` `bop3` в стэк. Так как он имеет  выше чем синус, то он  `op2` глубже.<br/>
 3 и  из стэка унарный  `bop3`.<br/>
Строка имеет вид `2 bop1 3 bop3`<br/>
Так как  строка , мы  цикл и  из стэка  все что там собрали.<br/>
Строка имеет вид `2 bop1 3 bop3 op2 bop2`<br/>

  и  это обратно в  символы:
`2 ~ 3 ~ s *`

  сову.

## `polish_solve(polish, op_stack, &result)`

В решении мы делим строку с помощью<br/>
&nbsp;char \*segment = `strtok`((char \*)str, " ");

И по   цифра это или нет.<br/>
&nbsp;status = (`sscanf`(segment, "%f", values) != 1);

Если это цифра, то мы  ее в массив float \*values.
Если нет, то находим  в стэке и  его каллбек,   в массив<br/>
&nbsp;c_ \*op = NULL;<br/>
&nbsp;status = `find_`(segment, stack, &op, true);<br/>
&nbsp;values -= (op->item_count - 1);<br/>
&nbsp;status = op->`execut`(values, values);<br/>

### `Пример`

`2 ~ 3 ~ s *`

 2 в массив ( пока  на месте);<br/>
 унарный .    равно 1.<br/>
 в <br/>
op->`execut`(values, values),<br/> текущий  и пишем туда -2.<br/>
Перед тем как считать тройку   `values` на 1.<br/>
 3<br/>
 унарный  и   элемент (3) и пишем на ее место  (-3).<br/>
 `sin` и   над  числом, на котором сейчас   (-3).<br/>
 из строки \*. Этот  требует 2 , а значит меняем <br/>
value -= (op->items_count - 1);<br/>
Таким образом мы  на месте -2. Снова <br/> op->`execut`(values, values),<br/>
  всегда должен быть в первом  массива values. Иначе - ошибка.

# Итог

Нет, мне есть чем ! Но правда в том, что   , будет являтся ~~вам во снах~~  задачей в , ввиду их  и  .
