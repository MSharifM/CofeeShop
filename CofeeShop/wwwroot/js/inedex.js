// GET ELEMENTS
const html = document.querySelector("html");
const toogleMoodBtns = document.querySelectorAll("#toggle_Mood");
const darkBtns = document.querySelectorAll("#dark__Btn");
const lightBtns = document.querySelectorAll("#light__Btn");
const darkmoodTitle = document.querySelector("#darkmood__Title");
const lightmoodTitle = document.querySelector("#lightmood__Title");
const subMenuMobile = document.querySelector("#sub__Menu__Mobile");
const subMenuBtn = document.querySelector("#sub__Menu__Btn");
const subMenuTitle = document.querySelector("#sub__Menu__Title");
const closeMenuMobilebtn = document.querySelector("#close__Menu__Mobile");
const openMenuMobilebtn = document.querySelector("#open__Menu__Mobile");
const mobileMenuBar = document.querySelector("#mobile__Menu__Bar");
const openshoppingcartbtn = document.querySelector("#open__Shopping__Mobile");
const closeshoppingcartbtn = document.querySelector("#close__Cart__Mobile");
const mobileShoppingCart = document.querySelector("#mobile__Shopping__cart");
const coverPage = document.querySelector("#cover__page");
const swiperslides = document.querySelectorAll(".swiper-slide");
const scrollDown = document.querySelector("#scrollDown");
const scrollTop = document.querySelector("#scrollTop");
// SWIPER SLIDE
const swiper = new Swiper(".swiper", {
  slidesPerView: 2,
  spaceBetween: 14,
  loop: true,
  breakpoints: {
    640: {
      slidesPerView: 3,
      spaceBetween: 14,
    },
    1024: {
      slidesPerView: 4,
      spaceBetween: 20,
    },
  },
  navigation: {
    nextEl: ".swiper-button-next-custom",
    prevEl: ".swiper-button-prev-custom",
  },
});
// TOOGLE MODD BY LOCAL STORAGE
if (
  localStorage.theme === "dark" ||
  (!("theme" in localStorage) &&
    window.matchMedia("(prefers-color-scheme: dark)").matches)
) {
  html.classList.add("dark");
  togglebtn();
} else {
  html.classList.remove("dark");
  togglebtn;
}

// TOOGLE MODD BY CLICK ON BUTTONS
toogleMoodBtns.forEach((buttons) => {
  buttons.addEventListener("click", () => {
    html.classList.toggle("dark");
    togglebtn();
  });
});
// SHOW DARK AND LIGHT BTN AND SAVE IN LOCAL STORAGE
function togglebtn() {
  if (html.classList.contains("dark")) {
    darkBtns.forEach((btn) => {
      btn.classList.add("hidden");
    });
    lightBtns.forEach((btn) => {
      btn.classList.remove("hidden");
    });
    darkmoodTitle.classList.add("hidden");
    lightmoodTitle.classList.remove("hidden");
    localStorage.setItem("theme", "dark");
  } else {
    darkBtns.forEach((btn) => {
      btn.classList.remove("hidden");
    });
    lightBtns.forEach((btn) => {
      btn.classList.add("hidden");
    });
    darkmoodTitle.classList.remove("hidden");
    lightmoodTitle.classList.add("hidden");
    localStorage.setItem("theme", "light");
  }
}
// SHOW SUB MENU FOR SHOPPING IN MOBLIE VIEW
subMenuBtn.addEventListener("click", (e) => {
  subMenuMobile.classList.toggle("hidden");
  changeColorShoppingSubMenu();
});
function changeColorShoppingSubMenu() {
  subMenuTitle.classList.toggle("text-zinc-600");
  subMenuTitle.classList.toggle("text-orange-300");
  subMenuTitle.classList.toggle("dark:text-white");
  subMenuTitle.classList.toggle("dark:text-orange-300");
  subMenuBtn.classList.toggle("rotate-90");
}
// OPEN MENU BAR IN MOBILE
openMenuMobilebtn.addEventListener("click", () => {
  mobileMenuBar.classList.remove("-right-full");
  mobileMenuBar.classList.add("right-0");
  coverPage.classList.toggle("hidden");
});
// CLOSE MENU BAR IN MOBILE
closeMenuMobilebtn.addEventListener("click", () => {
  closeMenuMobile();
});
coverPage.addEventListener("click", () => {
  closeMenuMobile();
  closeShoppingCart();
});
function closeMenuMobile() {
  mobileMenuBar.classList.add("-right-full");
  mobileMenuBar.classList.remove("right-0");
  coverPage.classList.add("hidden");
}
// OPEN SHOPPING CART IN MOBILE VIEW
openshoppingcartbtn.addEventListener("click", () => {
  mobileShoppingCart.classList.remove("-left-full");
  mobileShoppingCart.classList.add("left-0");
  coverPage.classList.toggle("hidden");
});
// CLOSE SHOPPING CART IN MOBILE VIEW
closeshoppingcartbtn.addEventListener("click", () => {
  closeShoppingCart();
});
function closeShoppingCart() {
  mobileShoppingCart.classList.remove("left-0");
  mobileShoppingCart.classList.add("-left-full");
  coverPage.classList.add("hidden");
}
// scrolls
scrollDown.addEventListener("click", () => {
  const footer = document.querySelector(".footer");
  footer.scrollIntoView({ behavior: "smooth" });
});
scrollTop.addEventListener("click", () => {
    const homeSection = document.querySelector(".header");
  homeSection.scrollIntoView({ behavior: "smooth" });
});
