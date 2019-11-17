export class TimePicker {
  constructor(date, options, selector) {    
    this.moment = moment(date),
    this.selector = selector;
    this.options = options;
    this.instance = null;
  }

  setDefaultInputValue() {
    let $input = $(this.selector);    
    $input.val(this.moment.format('LT'));
  }

  runTimePicker() {
    let instance = $(this.selector).timepicker(this.options);

    this.instance = instance[0].M_Timepicker;
    this.instance.amOrPm = this.moment.format('A');
  }

  getInstance() {    
    return this.instance;
  }

  getDate() {
    // Example value: 7:26 PM
    let time = $(this.selector).val(); 
    
    return moment(time, ['h:m a', 'H:m']).toDate();
  }
}