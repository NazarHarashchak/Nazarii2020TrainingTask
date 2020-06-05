var originUrl = window.location.origin;

var taskModel = {
    title: ko.observable(),
    taskDescription: ko.observable(),
    createdUserId: ko.observable(),
    assigneeUserId: ko.observable(),
    statusId: ko.observable(),
    users: ko.observableArray(),
    statuses: ko.observableArray(),
    submitHandler: function (form) {
        var task = {
            title: taskModel.title(),
            taskDescription: taskModel.taskDescription(),
            createdUserId: parseInt(taskModel.createdUserId()),
            assigneeUserId: parseInt(taskModel.assigneeUserId()),
            statusId: parseInt(taskModel.statusId())
        }
        console.log(task);
        $.ajax({
            url: originUrl + "/api/task/addnewtask",
            data: JSON.stringify(task),
            type: 'POST',
            contentType: "application/json"
        }).success(function () { window.location.replace("/index.html") });
    },
    changeHandler: function (model, event) {
        switch (event.target.name) {
            case 'title':
                model.title(event.target.value);
                break;
            case 'description':
                model.taskDescription(event.target.value);
                break;
            case 'author':
                model.createdUserId(event.target.value);
                break;
            case 'assignee':
                model.assigneeUserId(event.target.value);
                break;
            case 'status':
                model.statusId(event.target.value);
                break;
        }
    }
}

ko.dependentObservable(function () {
    $.ajax({
        url: originUrl + "/api/task/getusers",
        type: 'GET',
        success: function (data) {
            taskModel.users(data.result);
        }
    });
    $.ajax({
        url: originUrl + "/api/task/getstatuses",
        type: 'GET',
        success: function (data) {
            taskModel.statuses(data.result);
        }
    });
}, this) 

ko.applyBindings(taskModel);
