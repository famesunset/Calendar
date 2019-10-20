var AddEventAnimation = function(loader, content) {
    this.loader = loader;
    this.content = content;
};

AddEventAnimation.prototype.open = function() {
    var $content = $(this.content);
        
    $content.css('display', 'block');
    $content.animate({
        opacity: 1
    }, 50);
};

AddEventAnimation.prototype.close = function() {
    var $content = $(this.content);

    $content.animate({
        opacity: 0
    }, 50, () => $content.css('display', 'none'));
};

export { AddEventAnimation };