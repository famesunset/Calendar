export class DatePicker {
  constructor(options, selector) {    
    this.options = options;
    this.selector = selector;
    this.instance = null;
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