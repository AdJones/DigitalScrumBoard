var taskInTransition = false;
var animationSpeed = 400;
var timeRemainingBoxSizeChange = "10px";
var droppable;

$(function () {
    makeDraggableAndDroppable();
    clickResize();

    $(".draggableTask p:last-child").mousedown(function (event) {
        $(this).focus();
    });
});

function makeDraggableAndDroppable() {
    $(".draggableTask").draggable({
        disabled: false,
        drag: function (event, ui) {
            $(this).css("opacity", "0.6"); // Semi-transparent while being dragged
            $("#x").val(ui.position.left);
            $("#y").val(ui.position.top);
            droppable = $(this).data("current-droppable");
        },
        stop: function (event, ui) {
            event.stopPropagation();

            $(this).css("opacity", "1.0"); // Revert to fully opaque when dragging stopped

            $.ajax({ // AJAX call to persist co-ordinate updates after dropping task
                cache: false,
                url: "/Task/UpdateTaskCoords_Ajax",
                data: { id: event.target.id, left: ui.position.left, top: ui.position.top, droppedIntoCol: droppable.attr("id") }
            });
            droppable = undefined;
        },
        cursor: "move"
    });

    $(".droppableRow").droppable({
        over: function (event, ui) {
            ui.draggable.data("current-droppable", $(this));
        },
        drop: function (event, ui) {
            ui.draggable.removeData("current-droppable");
            $(this).animate({
                backgroundColor: '#FFFFCC'
            }, animationSpeed, function () {
                $(this).animate({
                    backgroundColor: 'white'
                }, animationSpeed)
            })
        }
    });
}

function clickResize() {
    $(".draggableTask").click(function (event, ui) {
        event.stopPropagation();
        if (!taskInTransition) {
            var currHeight = $(this).css('height');
            var currWidth = $(this).css('width');
            if (currHeight == "150px" && currWidth == "150px") {
                taskInTransition = true;
                $(this).animate({
                    width: "+=150", height: "+=150", fontSize: "+=10pt", padding: "+=25px", zIndex: "+=1"
                }, animationSpeed, function () {
                    taskInTransition = false;
                    $(this).draggable("option", "disabled", true);
                    $(this).attr('contenteditable', 'true');
                    $(this).css("opacity", "1");
                    $(this).focus();
                });
                $(this).children(':last').animate({
                    margin: "0 50px 50px 0",
                    height: "+=" + timeRemainingBoxSizeChange,
                    width: "+=" + timeRemainingBoxSizeChange
                });
            }
        }
    }).blur(function () {
        if ($(this).children(":focus").length == 0) {
            taskInTransition = true;
            $(this).animate({
                width: "-=150", height: "-=150", fontSize: "-=10pt", padding: "-=25px", zIndex: "-=1"
            }, animationSpeed, function () {
                $(this).draggable("option", "disabled", false);
                $(this).attr('contenteditable', 'false');
                taskInTransition = false;
                var updatedTaskText = "";
                var updatedTaskTime;
                var taskid = $(this).attr("id")
                $(this).children(":not(:last-child)").each(function () { updatedTaskText += $(this).html() + "\n" });
                try {
                    updatedTaskTime = $(this).children(":last-child").html().match(/\d/g)[0];
                }
                catch (ex) {
                    updatedTaskTime = undefined;
                }
                $.ajax({ // AJAX call to update text and time details after dropping task
                    cache: false,
                    url: "/Task/UpdateTaskDetails_Ajax",
                    data: { id: taskid, taskText: updatedTaskText, taskTime: updatedTaskTime }
                });
            })
            $(this).children(':last').animate({
                margin: "0 20px 20px 0",
                height: "-=" + timeRemainingBoxSizeChange,
                width: "-=" + timeRemainingBoxSizeChange
            });
        }
    });
}