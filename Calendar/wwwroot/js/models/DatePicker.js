var DatePicker = function(date) {
    this.setDefaultDate = true;
    this.defaultDate = date;
    this.firstDay = 1;
};

DatePicker.prototype.runDatePickers = function() {
    console.log(this);

    let $datepicker = $(".datepicker");
    $datepicker.datepicker(this);
};

export { DatePicker }