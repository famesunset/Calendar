class DatePicker {
  constructor(selector, options) {
    this.selector = selector;
    this.options = options;
    this.instance = null;
    this.date = new Date();
  }

  runDatePicker() {
    this.instance = $(this.selector).datepicker(this.options);
  }

  setDate(date) {
    var instance = this.getInstance();
    instance.setDate(date);
  }

  getInstance() {
    return this.instance[0].M_Datepicker;
  }

  getDate() {
    let instance = this.getInstance();
    return instance.date;
  }
}




export { DatePicker }