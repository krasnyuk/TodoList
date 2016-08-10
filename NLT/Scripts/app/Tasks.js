var taskUrl = 'api/_Task';

//CREATING NEW TASK
$(document).ready(function() {
    $('#taskCreate').click(function() {
        var task = {
            Title: $('#taskTitleCreate').val(),
            Description: $('#taskDescCreate').val(),
            ExpireDate: $('#taskExpireDateCreate').val(),
            Completed: $('#taskCompletedCreate').prop("checked"),
            TodoListId: $('#taskOflist').text()
        };
        $.ajax({
            url: taskUrl,
            type: 'POST',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(task),
            success: function (data, textStatus, xhr) {
                DetailsTodoList('Id' + $('#taskOflist').text());
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('error!');
            }
        });
    });
});

//THE LIST OF TASKS FOR TO-DO LIST
function DetailsTodoList(todoListId) {
    var getTasksForListUri = 'api/TaskInList/';
    $('#taskOflist').text(todoListId.substring(2));
    $.getJSON(getTasksForListUri + todoListId.substring(2), function(data) {
        WriteResponse(data);
    });
}

function WriteResponse(tasks) {
    var strResult = "<table  class=\"table table-hover col-md-6\">" +
        "<th>ID</th> <th>Title</th> <th>Description</th> <th>Expire date</th> <th>Completed</th> <th>Actions</th>";
    $.each(tasks, function (index, task) {
        strResult += "<tr><td>" + task.Id + "</td><td> " + task.Title + "</td><td>" + task.Description + "</td>" +
            "<td>" + task.ExpireDate.substring(0,10) + "</td> <td>" + ((task.Completed === true) ? '<a><span class=\"glyphicon glyphicon-ok\"></span></a>' : '<a><span class=\"glyphicon glyphicon-remove\"></span></a>') + "</td>" +
            "<td> " +
            "<a id='editItem' class=\"btn btn-xs btn-warning\" data-item='" + task.Id + "' onclick='EditItem(this);' >" +
                "<span class=\"glyphicon glyphicon-edit\"></span></a>" +
            "<a id='delItem' class=\"btn btn-xs btn-danger\" data-item='" + task.Id + "' onclick='DeleteItem(this);'>" +
                "<span class=\"glyphicon glyphicon-trash\"></span></a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}


//LOADING DATA INTO UPDATE PANEL
function EditItem(el) {
    var id = $(el).attr('data-item');

    $.getJSON(taskUrl + '/' + id, function (data) {
        $('#updateTaskId').text(data.Id);
        $('#taskTitleUpdate').val(data.Title);
        $('#taskDescUpdate').val(data.Description);
        $('#taskExpireDateUpdate').val(data.ExpireDate);
        $('#taskCompletedUpdate').val(data.Completed);
    });
};

//DELETE TASK
function DeleteItem(el) {
    var id = $(el).attr('data-item');
    $.ajax({
        url: taskUrl + '/' + id,
        type: 'DELETE',
        success: function (data, textStatus, xhr) {
            DetailsTodoList('Id' + $('#taskOflist').text());
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error while deleting! ' + textStatus);
        }
    });
};

//UPDATE TASK
function sendAjaxUpdateTask() {
    var task = {
        Id: $('#updateTaskId').text(),
        Title: $('#taskTitleUpdate').val(),
        Description: $('#taskDescUpdate').val(),
        ExpireDate: $('#taskExpireDateUpdate').val(),
        Completed: $('#taskCompletedUpdate').prop("checked"),
        TodoListId: $('#taskOflist').text()
    };
    $.ajax({
        url: taskUrl + '/' + task.Id,
        type: 'PUT',
        dataType: 'json',
        data: task,
        success: function (data, textStatus, xhr) {
            DetailsTodoList('Id' + task.TodoListId);
        }
    });
}