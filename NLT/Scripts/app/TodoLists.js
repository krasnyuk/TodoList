
var getToDoListUri = "api/TodoLists";

function getAllTodoLists() {
    // Send an AJAX request
    $('#Todos').empty();
    $.getJSON(getToDoListUri,
        function (data) {
            // On success, 'data' contains a list of toDoLists.
            $.each(data, function (key, item) {
                // Add a list item for the ToDoList.
                $('<li>', { text: formatItem(item) })
                    .append('<button id=\"Id' + item.Id + '\" onclick="DeleteTodoList(this.id)" class="btn btn-xs btn-danger pull-right"><span class="glyphicon glyphicon-trash"></span></button>')
                    .append('<button id=\"Id' + item.Id + '\" onclick="UpdateTodoList(this.id)" class="btn btn-xs btn-warning pull-right"><span class="glyphicon glyphicon-edit"></span></button>')
                    .append('<button id=\"Id' + item.Id + '\" onclick="DetailsTodoList(this.id)" class="btn btn-xs btn-success pull-right"><span class="glyphicon glyphicon-list"></span></button>')
                    .appendTo($("#Todos"))
                    .addClass("list-group-item");
            });
        });
}
$(document).ready(function () {
    getAllTodoLists();

    //Creating new To-Do list
    $('#toDoCreate').click(function() { //on button click
        var toDoList = new Object();
        toDoList.Title = $('#toDoTitle').val();
        toDoList.Date = $('#toDoDate').val();
        //ajax request
        $.ajax({
            url: getToDoListUri,
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(toDoList),
            success: function (data, textStatus, xhr) {
                getAllTodoLists();
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('error!');
            }
        });
        });
});

//UPDATE REQUEST FOR TO-DO LIST
function sendAjaxUpdate() {
    var list = {
            Id: $('#updateListId').text(),
            Title: $('#toDoTitleUpdate').val(),
            Date: $('#toDoDateUpdate').val()
        };
    $.ajax({
        url: getToDoListUri + '/' + list.Id,
        type: 'PUT',
        dataType: 'json',
        data: list,
        success: function (data, textStatus, xhr) {
            getAllTodoLists();
        }
    });
}

//LOADING DATA INTO PANEL BEFORE UPDATE REQUEST
function UpdateTodoList(listId) {
    $.ajax(
        {
            url: getToDoListUri + '/' + listId.substring(2),
            type: 'GET',
            success: function(data, textStatus, xhr) {
                $('#toDoTitleUpdate').val(data.Title);
                $('#toDoDateUpdate').val(data.Date);
                $('#updateListId').text(data.Id);
            }
        }
        );
}
//DELETE REQUEST FOR TO-DO LIST (ALL RELATED TASKS WOULD BE DELETED AT BACK-END BY TO-DO LIST ID)
function DeleteTodoList(listId) {
    var resultId = +listId.substring(2);
    $.ajax({
        url: getToDoListUri + '/' + resultId,
        type: 'DELETE',
        success: function (data, textStatus, xhr) {
            getAllTodoLists();
        },
        error: function (xhr, textStatus, errorThrown) {
            getAllTodoLists();
        }
    });

}

function formatItem(item) {
    return '# ' + item.Id + ' | ' + item.Title + ' | Created: ' + item.Date.substring(0,10);
}

function find() {
    var id = $('#todoId').val();
    $.getJSON(getToDoListUri + '/' + id)
        .done(function (data) {
            $('#todoItem').text(formatItem(data));
        })
        .fail(function (jqXhr, textStatus, err) {
            $('#todoItem').text('Error: ' + err);
        });
}

