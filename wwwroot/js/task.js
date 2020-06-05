var originUrl = window.location.origin;
var urlParams = new URLSearchParams(window.location.search);
var id = urlParams.get("taskid");

function setValues(value) {
    taskModel.id(value.id);
    taskModel.title(value.title);
    taskModel.description(value.taskDescription);
    taskModel.author(value.createdUserName);
    taskModel.assigneeUser(value.assigneeUserName);
    taskModel.dueDate(value.dueDate);
    taskModel.status(value.activeStatus);
}
var taskModel = {
    id: ko.observable(),
    title: ko.observable(),
    description: ko.observable(),
    author: ko.observable(),
    assigneeUser: ko.observable(),
    dueDate: ko.observable(),
    status: ko.observable()
}

ko.dependentObservable(function () {
    $.ajax({
        url: originUrl + "/api/task/gettask/" + id,
        type: 'GET',
        context: this,
        success: function (data) {
            console.log(data);
            setValues(data);
        }
    });
}, this)

ko.applyBindings(taskModel);
