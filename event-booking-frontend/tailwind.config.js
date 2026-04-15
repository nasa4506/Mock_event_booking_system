/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: "class",
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        "primary-container": "#1e40af",
        "on-tertiary-container": "#ffa583",
        "on-surface": "#191c1e",
        "secondary": "#505f76",
        "background": "#f7f9fb",
        "surface-variant": "#e0e3e5",
        "on-secondary": "#ffffff",
        "inverse-surface": "#2d3133",
        "surface-container": "#eceef0",
        "on-tertiary-fixed-variant": "#802a00",
        "on-primary-fixed": "#001453",
        "primary": "#00288e",
        "outline": "#757684",
        "tertiary-fixed-dim": "#ffb59a",
        "on-secondary-container": "#54647a",
        "tertiary-fixed": "#ffdbce",
        "secondary-fixed-dim": "#b7c8e1",
        "surface-tint": "#3755c3",
        "surface-container-lowest": "#ffffff",
        "surface-container-high": "#e6e8ea",
        "secondary-container": "#d0e1fb",
        "tertiary": "#611e00",
        "inverse-on-surface": "#eff1f3",
        "tertiary-container": "#872d00",
        "outline-variant": "#c4c5d5",
        "error-container": "#ffdad6",
        "surface-container-low": "#f2f4f6",
        "on-primary": "#ffffff",
        "on-primary-container": "#a8b8ff",
        "secondary-fixed": "#d3e4fe",
        "inverse-primary": "#b8c4ff",
        "surface-dim": "#d8dadc",
        "primary-fixed-dim": "#b8c4ff",
        "on-error-container": "#93000a",
        "surface": "#f7f9fb",
        "on-primary-fixed-variant": "#173bab",
        "on-secondary-fixed-variant": "#38485d",
        "surface-bright": "#f7f9fb",
        "on-tertiary": "#ffffff",
        "primary-fixed": "#dde1ff",
        "on-secondary-fixed": "#0b1c30",
        "on-error": "#ffffff",
        "error": "#ba1a1a",
        "on-background": "#191c1e",
        "surface-container-highest": "#e0e3e5",
        "on-tertiary-fixed": "#380d00",
        "on-surface-variant": "#444653"
      },
      borderRadius: {
        DEFAULT: "0.25rem",
        lg: "0.5rem",
        xl: "0.75rem",
        full: "9999px"
      },
      fontFamily: {
        headline: ["Manrope", "sans-serif"],
        body: ["Inter", "sans-serif"],
        label: ["Inter", "sans-serif"]
      }
    },
  },
  plugins: [
    require("@tailwindcss/forms"),
    require("@tailwindcss/container-queries")
  ],
}
