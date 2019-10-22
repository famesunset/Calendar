var Component = function(html) {
    this.html = html;
};

Component.prototype.setDataContent = function(content) {
    this.html = $(this.html).attr('data-content', content);
}

Component.prototype.appentTo = function(container) {
    $(container).append(this.html);
};  

export { Component };