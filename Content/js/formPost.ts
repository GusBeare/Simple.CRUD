
'use strict';

const FORM_NAME = "crud-form";
const API_URL = "/data/modify";
const RESPONSE_CONTAINER = "response_display";
const RESULTS_CONTAINER = "results_display";

/**
 * Checks that an element has a non-empty `name` and `value` property.
 * @@param  {Element} element  the element to check
 * @@return {Bool}             true if the element is an input, false if not
 */
function isValidElement(element: HTMLInputElement) {
    return element.name && element.value;
};

/**
 * Checks if an element’s value can be saved (e.g. not an unselected checkbox).
 * @@param  {Element} element  the element to check
 * @@return {Boolean}          true if the value should be added, false if not
 */
function isValidValue(element: HTMLInputElement) {
    return !['checkbox', 'radio'].includes(element.type) || element.checked;
};

/**
 * Checks if an input is a checkbox, because checkboxes allow multiple values.
 * @@param  {Element} element  the element to check
 * @@return {Boolean}          true if the element is a checkbox, false if not
 */
function isCheckbox(element: HTMLInputElement) {
    return element.type === 'checkbox';
};

/**
 * Checks if an input is a `select` with the `multiple` attribute.
 * @@param  {Element} element  the element to check
 * @@return {Boolean}          true if the element is a multiselect, false if not
 */
function isMultiSelect(element: HTMLSelectElement) {
    return element.options && element.multiple;
};

/**
 * Retrieves the selected options from a multi-select as an array.
 * @@param  {HTMLOptionsCollection} options  the options for the select
 * @@return {Array}                          an array of selected option values
 */
function getSelectValues(options: HTMLInputElement) {
    return [].reduce.call(options, (values, option) => option.selected ? values.concat(option.value) : values, []);
};

/**
 * A more verbose implementation of `formToJSON()` to explain how it works.
 *
 * NOTE: This function is unused, and is only here for the purpose of explaining how
 * reducing form elements works.
 *
 * @@param  {HTMLFormControlsCollection} elements  the form elements
 * @@return {Object}                               form data as an object literal
 */
function formToJSON_deconstructed(elements: any) {

    // This is the function that is called on each element of the array.
    const reducerFunction = (data, element) => {

        // Add the current field to the object.
        data[element.name] = element.value;

        // For the demo only: show each step in the reducer’s progress.
        console.log(JSON.stringify(data));

        return data;
    };

    // This is used as the initial value of `data` in `reducerFunction()`.
    const reducerInitialValue = {};

    // To help visualize what happens, log the inital value, which we know is `{}`.
    console.log('Initial `data` value:', JSON.stringify(reducerInitialValue));

    // Now we reduce by `call`-ing `Array.prototype.reduce()` on `elements`.
    const formData = [].reduce.call(elements, reducerFunction, reducerInitialValue);

    // The result is then returned for use elsewhere.
    return formData;
};

/**
 * Retrieves input data from a form and returns it as a JSON object.
 * @@param  {HTMLFormControlsCollection} elements  the form elements
 * @@return {Object}                               form data as an object literal
 */
function formToJSON(elements: any) {
    return [].reduce.call(elements, (data, element) => {

        // Make sure the element has the required properties and should be added.
        if (isValidElement(element) && isValidValue(element)) {

            /*
             * Some fields allow for more than one value, so we need to check if this
             * is one of those fields and, if so, store the values as an array.
             */
            if (isCheckbox(element)) {
                data[element.name] = (data[element.name] || []).concat(element.value);
            } else if (isMultiSelect(element)) {
                data[element.name] = getSelectValues(element);
            } else {
                data[element.name] = element.value;
            }
        }

        return data;
    }, {});
};

/**
 * A handler function to prevent default submission and run our custom script.
 * @@param  {Event} event  the submit event triggered by the user
 * @@return {void}
 */
function handleFormSubmit(event: any)
    : void {

    // Stop the form from submitting since we’re handling that with AJAX.
    event.preventDefault();

    // Call our function to get the form data.
    const data = formToJSON(document.getElementsByClassName(FORM_NAME)[0]);

    // Demo only: print the form data onscreen as a formatted JSON object.
    const dataContainer = document.getElementsByClassName(RESULTS_CONTAINER)[0];

    // Use `JSON.stringify()` to make the output valid, human-readable JSON.
    dataContainer.textContent = JSON.stringify(data, null, "  ");

    // Post the data to our handler
    var http = new XMLHttpRequest();
    http.open("POST", API_URL, true);

    let token;
    for (let key in data) {
        if (data.hasOwnProperty(key)) {
            if (key === 'NCSRF') {
                token = data[key];
                // console.log(token)
            }
        }
    }

    // Send the proper header information along with the request
    http.setRequestHeader("Content-type", "application/json");
    http.setRequestHeader("NCSRF", token);

    http.onreadystatechange = () => { //Call a function when the state changes.
        if (http.readyState === 4 && http.status === 200) {

            var responseContainer = document.getElementsByClassName(RESPONSE_CONTAINER)[0];
            console.log(RESPONSE_CONTAINER);
            responseContainer.textContent = http.responseText;

        }
        else if (http.readyState === 2 && http.status === 403) {

            var responseContainer = document.getElementsByClassName(RESPONSE_CONTAINER)[0];
            console.log(RESPONSE_CONTAINER);
            responseContainer.textContent = http.responseText;
        }
    }

    // console.log(JSON.stringify(data, null, "  "));
    http.send(JSON.stringify(data, null, "  "));
};

/*
 * This is where things actually get started. We find the form element using
 * its class name, then attach the `handleFormSubmit()` function to the 
 * `submit` event.
 */
var form = document.getElementsByClassName(FORM_NAME)[0];


// only try to attach if a form was found
if (typeof (form) != 'undefined' && form != null) {

    // Some browsers such as IE8 do not support addEventListener so we must use attachEvent
    if (form.addEventListener) {
        form.addEventListener('submit', handleFormSubmit);
    } else {
        (window as any).attachEvent("onsubmit", handleFormSubmit);
    }

}

