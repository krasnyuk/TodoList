
var getToDoListUri = "api/TodoLists";

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(getToDoListUri)
        .done(function (data) {
            // On success, 'data' contains a list of toDoLists.
            $.each(data, function (key, item) {
                // Add a list item for the ToDoList.
                $('<li>', { text: formatItem(item) }).appendTo($("#Todos")).addClass("list-group-item");
            });
        });

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
            data: toDoList,
            success: function (data, textStatus, xhr) {
                console.log(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error in Operation');
            }
        });
        });
});

function formatItem(item) {
    return '# ' + item.Id + ' ' + item.Title + ' Created: ' + item.Date;
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

