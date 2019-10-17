var DatePicker = function(selector, options) {
    this.selector = selector;
    this.options = options;
    this.instance = null;
    this.date = new Date();
};

DatePicker.prototype.runDatePicker = function() {
    this.instance = $(this.selector).datepicker(this.options);
};

DatePicker.prototype.getInstance = function() {
    return this.instance[0].M_Datepicker;
};

DatePicker.prototype.getDate = function() {
    let instance = this.getInstance();

    return instance.date;
}


export { DatePicker }