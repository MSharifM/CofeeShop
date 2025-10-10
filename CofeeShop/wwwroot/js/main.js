const $ = document;
const formTitle = $.querySelector(".form-title");
const headerButtons = $.querySelectorAll(".header-btn");
const loginButton = $.querySelector(".login-btn");
const registerButton = $.querySelector(".register-btn");
const navigateLinks = $.querySelectorAll(".navigate-link");
const loginForm = $.querySelector(".login-form");
const registerForm = $.querySelector(".register-form");
const eyeButtons = $.querySelectorAll(".eye-btn");
const alertTexts = $.querySelectorAll(".alert-text");

//login elements
const loginEmailInput = $.getElementById("login-email-input");
const loginEmailAlert = $.getElementById("login-email-alert");
const loginPasswordInput = $.getElementById("login-password-input");
const loginPasswordAlert = $.getElementById("login-password-alert");

// register elements
const registerEmailInput = $.getElementById("register-email-input");
const registerEmailAlert = $.getElementById("register-email-alert");
const registerPasswordInput = $.getElementById("register-password-input");
const registerPasswordAlert = $.getElementById("register-password-alert");
const registerConfirmPasswordInput = $.getElementById(
  "register-confirm-password-input"
);
const registerConfirmPasswordAlert = $.getElementById(
  "register-confirm-password-alert"
);

// TOGGLE FORMS
navigateLinks.forEach((link) => {
  link.addEventListener("click", (e) => {
    e.preventDefault();

    const dataLink = link.dataset.link;
    if (dataLink === "login") {
      loginButton.click();
    } else if (dataLink === "register") {
      registerButton.click();
    }
  });
});

//------------- SHOW LOGIN FORM
loginButton.addEventListener("click", () => {
  formTitle.innerHTML = "Login Form";
  // show login data inputs
  loginForm.classList.add("active");
  registerForm.classList.remove("active");

  // set active class login button
  loginButton.classList.add("active");
  registerButton.classList.remove("active");
});

//------------- SHOW REGISTER FORM
registerButton.addEventListener("click", () => {
  formTitle.innerHTML = "Register Form";
  // show register data inputs
  registerForm.classList.add("active");
  loginForm.classList.remove("active");

  // set active class register button
  registerButton.classList.add("active");
  loginButton.classList.remove("active");
});

// SHOW OR HIDE PASSWORDS
eyeButtons.forEach((eye) => {
  eye.addEventListener("click", () => {
    const input = eye.previousElementSibling;

    if (input.type === "password") {
      input.type = "text";
      eye.firstElementChild.classList.replace("fa-eye", "fa-eye-slash");
    } else {
      input.type = "password";
      eye.firstElementChild.classList.replace("fa-eye-slash", "fa-eye");
    }
  });
});

// VALIDATE LOGIN FORM
loginForm.addEventListener("submit", (e) => {
  if (!validateLoginForm()) {
    e.preventDefault();
  }
});

// validate login email
loginEmailInput.addEventListener("input", () => {
  const emailFormat =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  if (!loginEmailInput.value.match(emailFormat)) {
    loginEmailInput.classList.add("wrong-input");
    loginEmailAlert.innerText = "Please enter a valid email";
  } else {
    loginEmailInput.classList.remove("wrong-input");
    loginEmailAlert.innerText = "";
  }
});

// validate login password
loginPasswordInput.addEventListener("input", () => {
  if (loginPasswordInput.value.length < 6) {
    loginPasswordInput.classList.add("wrong-input");
    loginPasswordAlert.innerText = "Enter more than 6 character!";
  } else {
    loginPasswordInput.classList.remove("wrong-input");
    loginPasswordAlert.innerText = "";
  }
});

const validateLoginForm = () => {
  // email validation
  const emailFormat =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  if (!loginEmailInput.value.match(emailFormat)) {
    loginEmailInput.classList.add("wrong-input");
    loginEmailAlert.innerText = "Please enter a valid email";
    return false;
  }

  // password validation
  if (loginPasswordInput.value.length < 6) {
    loginPasswordInput.classList.add("wrong-input");
    loginPasswordAlert.innerText = "Enter more than 6 character!";
    return false;
  }

  return true; // if inputs was correct
};

// VALIDATE REGISTER FORM
registerForm.addEventListener("submit", (e) => {
  if (!validateRegisterForm()) {
    e.preventDefault();
  }
});

// validate register email
registerEmailInput.addEventListener("input", () => {
  const emailFormat =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  if (!registerEmailInput.value.match(emailFormat)) {
    registerEmailInput.classList.add("wrong-input");
    registerEmailAlert.innerText = "Please enter a valid email";
  } else {
    registerEmailInput.classList.remove("wrong-input");
    registerEmailAlert.innerText = "";
  }
});

// validate register password
registerPasswordInput.addEventListener("input", () => {
  if (registerPasswordInput.value.length < 6) {
    registerPasswordInput.classList.add("wrong-input");
    registerPasswordAlert.innerText = "Enter more than 6 character!";
  } else {
    registerPasswordInput.classList.remove("wrong-input");
    registerPasswordAlert.innerText = "";
  }
});

// validate register confirm password
registerConfirmPasswordInput.addEventListener("input", () => {
  if (registerConfirmPasswordInput.value.length < 6) {
    registerConfirmPasswordInput.classList.add("wrong-input");
    registerConfirmPasswordAlert.innerText = "Enter more than 6 character!";
  } else if (
    registerConfirmPasswordInput.value !== registerPasswordInput.value
  ) {
    registerConfirmPasswordInput.classList.add("wrong-input");
    registerConfirmPasswordAlert.innerText = "Passwords Does'nt Match!";
  } else {
    registerConfirmPasswordInput.classList.remove("wrong-input");
    registerConfirmPasswordAlert.innerText = "";
  }
});

// validate register form
const validateRegisterForm = () => {
  // validate email
  const emailFormat =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  if (!registerEmailInput.value.match(emailFormat)) {
    registerEmailInput.classList.add("wrong-input");
    registerEmailAlert.innerText = "Please enter a valid email";
    return false;
  }

  // validate password
  if (registerPasswordInput.value.length < 6) {
    registerPasswordInput.classList.add("wrong-input");
    registerPasswordAlert.innerText = "Enter more than 6 character!";
    return false;
  }

  // validate confirm password
  if (registerConfirmPasswordInput.value.length < 6) {
    registerConfirmPasswordInput.classList.add("wrong-input");
    registerConfirmPasswordAlert.innerText = "Enter more than 6 character!";
    return false;
  } else if (
    registerConfirmPasswordInput.value !== registerPasswordInput.value
  ) {
    registerConfirmPasswordInput.classList.add("wrong-input");
    registerConfirmPasswordAlert.innerText = "Passwords Does'nt Match!";
    return false;
  }

  return true; // if inputs was correct
};

// reset form alertwhen switch to another forms
headerButtons.forEach((btn) => {
  btn.addEventListener("click", () => {
    alertTexts.forEach((alert) => {
      alert.innerText = "";
    });
  });
});
