import { RepeatDropdown } from '../models/RepeatDropdown.js';

$(function() {
    $('.more-options-btn').click(() => {
        let options = document.getElementById("options");

        // Options are already open
        let isOpenOptions = options != null;
        if (isOpenOptions) {            
            return;
        }            

        // Load more options to container
        $('#more-options').load('LoadView/EventMoreOptions', () => {
            let dropdown = new RepeatDropdown(new Date(), { constrainWidth: false });

            dropdown.setEveryWeekText(`#every-week`);
            dropdown.setEveryMonthText('#every-month');
            dropdown.setEveryYearText('#every-year');

            dropdown.runDropDown('.dropdown-trigger');

            $('.dropdown-content a').click((event) => {
                dropdown.clickHandler(event);
            });
            
            $('.modal').modal();
        });        
    });
});