﻿var taskInTransition = false;
var animationSpeed = 400;
var timeRemainingBoxSizeChange = "10px";

// Start-up function
$(function () {
    makeDraggableAndDroppable();
    clickResize();

    $(".draggableTask p:last-child").mousedown(function (event) {
        $(this).focus();
    });
});

// Window resizing features
$(window).resize(function () {
    if (this.resizeTO) clearTimeout(this.resizeTO);
    this.resizeTO = setTimeout(function () {
        $(this).trigger('resizeEnd');
    }, 500);
});

$(window).bind('resizeEnd', function () {
    var width = $(window).width();
    if (width > 1000) {
        clickResize();
    }
    else {
        $(".draggableTask").off('click');
        $(".draggableTask").off('blur');
    }
});

// Function to make tasks draggable and columns droppable
function makeDraggableAndDroppable() {
    var l_nScrollTop = $(window).scrollTop();
    var l_nScrollLeft = $(window).scrollLeft();
    var droppable;

    $(".draggableTask").draggable({
        disabled: false,
        start: function () {
            l_nScrollTop = $(window).scrollTop();
            l_nScrollLeft = $(window).scrollLeft();
        },
        drag: function (event, ui) {
            $(window).scrollTop(l_nScrollTop);
            $(window).scrollLeft(l_nScrollLeft);
            $(this).css("opacity", "0.6"); // Semi-transparent while being dragged
            $("#x").val(ui.position.left);
            $("#y").val(ui.position.top);
            droppable = $(this).data("current-droppable");
        },
        stop: function (event, ui) {
            event.stopPropagation();

            $(this).css("opacity", "1.0"); // Revert to fully opaque when dragging stopped

            moveTask($(this), $(droppable));

            droppable = undefined;
        },
        cursor: "move"
    });

    $(".droppableCol").droppable({
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

// Function to move a draggable task into a droppable column
function moveTask(draggable, droppable) {
    // Move the task to a different parent TD
    var droppableId = droppable.attr("id");
    var storyIdFromDraggable = draggable.data("storyid");

    // Find the child DIV to which the task should be appended
    var divToAppend;
    droppable.children('div').each(function () {
        if ($(this).data("storyid") == storyIdFromDraggable) {
            divToAppend = $(this);
        }
    });

    draggable.appendTo(divToAppend);

    // Move the task in the DB
    var myLeftAsPercent = draggable.position().left / draggable.parent().parent().parent().width() * 100;
    var myTop = draggable.position().top;

    draggable.css('left', myLeftAsPercent + "%");
    $.ajax({ // AJAX call to persist co-ordinate updates after dropping task
        cache: false,
        url: "/Task/UpdateTaskCoords_Ajax",
        data: { id: draggable.attr("id"), left: myLeftAsPercent, top: myTop, droppedIntoCol: droppable.attr("id") }
    });
}

// Function to enable the on-click and on-blur resizing of tasks
function clickResize() {
    $(".draggableTask").off('click');
    $(".draggableTask").off('blur');
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
        var aWidth = $(this).css("width");
        if ($(this).children(":focus").length == 0 && $(this).width != "150px" && $(this).height != "150px") {
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