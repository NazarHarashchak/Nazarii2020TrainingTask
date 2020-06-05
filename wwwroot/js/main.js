var originUrl = window.location.origin;
function task(id, title, description, author, assignee, dueDate, status) {
    this.id = (id);
    this.title = (title);
    this.description = (description);
    this.author = (author);
    this.assignee = (assignee);
    this.dueDate = (dueDate);
    this.status = (status);
}
var taskModel = {
    taskList: ko.observableArray(),
    deleteHandler: function (model, event) {
        var id = event.target.name;
        console.log(id)
        $.ajax({
            url: originUrl + "/api/task/deletetask/" + id,
            type: 'DELETE',
        context: this
        }).success(function (data) { taskModel.taskList(data); });
    }
}

ko.dependentObservable(function () {
    $.ajax({
        url: originUrl + "/api/task/gettasks",
        type: 'GET',
        context: this,
        success: function (data) {
            console.log(data);
            taskModel.taskList(data);
        }
    });
}, this)

ko.applyBindings(taskModel);
