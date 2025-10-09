/** @type {import('tailwindcss').Config} */
export const content = ["./public/*.html"];
export const darkMode = "class";
export const theme = {
  extend: {
    colors: {
      brown: {
        100: "#ECE0D1",
        300: "#DBC1AC",
        600: "#967259",
        900: "#634832",
      },
    },
    boxShadow: {
      normal: "0px 1px 10px rgba(0,0,0,0.05)",
    },
    borderRadius: {
      "4xl": "2rem",
    },
    fontFamily: {
      "Dana-regular": "Dana-regular",
      "Dana-Medium": "Dana-Medium",
      "Dana-demiBold": "Dana-demiBold",
      "Morabba-Light": "Morabaa-Light",
      "Morabba-Medium": "Morabaa-Medium",
      "Morabba-Bold": "Morabaa-Bold",
    },
    letterSpacing: {
      tithest: "-0.065em",
    },
    spacing: {
      25: "6.25rem",
      30: "7.5rem",
      50: "12.5rem",
    },
    container: {
      center: true,
      padding: {
        DEFAULT: "1rem",
        lg: "0.625rem",
      },
    },
    backgroundImage: {
      homemobile: "url(../images/headerBgMobile.webp)",
      homedesctop: "url(../images/headerBgDesktop.webp)",
    },
  },
  screens: {
    xs: "480px",
    sm: "640px",
    md: "768px",
    lg: "1024px",
    xl: "1280px",
  },
};
export const plugins = [
  function ({ addVariant }) {
    addVariant("child", "& > *");
    addVariant("child-hover", "& > *:hover");
  },
];
