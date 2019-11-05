class DatePicker {
  constructor(selector, options) {
    this.selector = selector;
    this.options = options;
    this.instance = null;
    this.date = new Date();
  }

  runDatePicker() {
    let instance = $(this.selector).datepicker(this.options);
    this.instance = instance[0].M_Datepicker;
  }

  open() {
    this.instance.open();
  }

  setDate(date) {    
    this.instance.setDate(date);
  }

  getInstance() {
    return this.instance;
  }

  getDate() {    
    return this.instance.date;
  }
}

export { DatePicker }