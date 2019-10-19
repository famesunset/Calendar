var MoreOptionsAnimation = function (wrapper, content) {
    this.wrapper = wrapper;
    this.content = content;
};

MoreOptionsAnimation.prototype.runAnimation = function () {
    var $optionsForm = $(this.wrapper);
    var $options = $(this.content);

    $optionsForm.animate({
        height: `+=${$options.height()}`
    }, 100, () => {
        $options.css('display', 'block');
        $options.animate({
            opacity: 1
        }, 150);
    });
};

export { MoreOptionsAnimation };