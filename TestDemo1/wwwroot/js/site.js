

//$('#studentForm').on('AddStandard', function (e) {
//    var isValid = true;



function validateFormFields() {
    var isValid = true;

    var StudentName = $('#StudentName').val();
    var StudentNameRegex = /^[a-zA-Z\s]+$/;
    if (StudentName === '') {
        $('#StudentNameError').text('Student Name is Required').show();
        isValid = false;
    }
    else if (!StudentNameRegex.test(StudentName)) {
        $('#StudentNameError').text('Student Name can only contain letters and spaces.').show();
        isValid = false;
    }
    else {
        $('#StudentNameError').text('').hide();
    }

    var Standard = $('#Standard').val();
    if (Standard === '') {
        $('#StandardError').text('Standard is Required').show();
        isValid = false;
    }
    else {
        $('#StandardError').text('').hide();
    }

    var Section = $('#Section').val();
    if (Section === '') {
        $('#SectionError').text('Section is Required').show();
        isValid = false;
    }
    else {
        $('#SectionError').text('').hide();
    }

    var Age = $('#Age').val();
    var AgeRegex = /^[1-9][0-9]?$/;
    if (Age === '') {
        $('#AgeError').text('Age is Required').show();
        isValid = false;
    }
    else if (!AgeRegex.test(Age)) {
        $('#AgeError').text('Please Enter Valid Age.').show();
        isValid = false;
    }
    else {
        $('#AgeError').text('').hide();
    }

    var Gender = $('input[name="Gender"]:checked').val();
    if (Gender === undefined) {
        $('#GenderError').text('Gender is Required').show();
        isValid = false;
    }
    else {
        $('#GenderError').text('').hide();
    }

    var Address = $('#Address').val();
    if (Address === '') {
        $('#AddressError').text('Address is Required').show();
        isValid = false;
    }
    else {
        $('#AddressError').text('').hide();
    }

    return isValid;
}

// type something and hide error message
$('#StudentName').on('input', function () {
    var StudentName = $(this).val();
    if (StudentName !== '') {
        $('#StudentNameError').text('').hide();
    }
});

$('#Standard').on('change', function () {
    var Standard = $(this).val();
    if (Standard !== '') {
        $('#StandardError').text('').hide();
    }
});

$('#Section').on('change', function () {
    var Section = $(this).val();
    if (Section !== '') {
        $('#SectionError').text('').hide();
    }
});

$('#Age').on('input', function () {
    var Age = $(this).val();
    if (Age !== '') {
        $('#AgeError').text('').hide();
    }
});

$('input[name="Gender"]').on('change', function () {
    var Gender = $('input[name="Gender"]:checked').val();
    if (Gender !== undefined) {
        $('#GenderError').text('').hide();
    }
});

$('#Address').on('input', function () {
    var Address = $(this).val();
    if (Address !== '') {
        $('#AddressError').text('').hide();
    }
});

function removeValidationError() {

    // Normalize input tags
    $('#StudentName').css('border-color', '');
    $('#Standard').css('border-color', '');
    $('#Section').css('border-color', '');
    $('#Age').css('border-color', '');
    $('input[name="Gender"]').css('border-color', '');
    $('#Address').css('border-color', '');

    // Clear previous error messages
    $('#StudentNameError').text('').hide();
    $('#StandardError').text('').hide();
    $('#SectionError').text('').hide();
    $('#AgeError').text('').hide();
    $('#GenderError').text('').hide();
    $('#AddressError').text('').hide();
}
