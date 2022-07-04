var array = ["Не правильный", "Правильный"];


function newField() {
    // определяем контейнер для хранения полей с вопросами
    let containerA = document.getElementById("answers");
    let containerB = document.getElementById("isAnswer");

    // получаем текущее количество input (полей для вопросов)
    let fieldCountA = containerA.getElementsByTagName("input").length;
    // увеличиваем Id для нового поля
    let nextFieldIdA = fieldCountA + 1;

    // здесь добавляем элемент, который будет хранить input (в моем случае, у вас может быть по другому или вообще не быть его)
    let divAnswer = document.createElement("li");


    let divIsAnswer = document.createElement("li");


    // создаем новое поле с новым id, name ДОЛЖЕН СОВПАДАТЬ С ИМЕНЕМ ПОЛЯ В МОДЕЛИ!!!
    let fieldA = document.createElement("input");
    fieldA.setAttribute("class", "form-control");
    fieldA.setAttribute("id", "Answers[" + nextFieldIdA + "]");
    fieldA.setAttribute("name", "Answers");
    fieldA.setAttribute("type", "text");
    fieldA.setAttribute("placeholder", "Введите текст ответа");


    let fieldB = document.createElement("select");
    fieldB.setAttribute("class", "form-control");
    fieldB.setAttribute("id", "IsAnswer[" + nextFieldIdA + "]");
    fieldB.setAttribute("name", "IsAnswer");

    for (var i = 0; i < array.length; i++) {
        var option = document.createElement("option");
        option.value = array[i];
        option.text = array[i];
        fieldB.appendChild(option);
    }


    // добавляем поле в <div class="form-group"></div>
    divAnswer.appendChild(fieldA);
    divIsAnswer.appendChild(fieldB);
    containerA.appendChild(divAnswer);
    containerA.appendChild(divIsAnswer);
}