import { createTheme, ThemeOptions } from '@mui/material/styles'

// Augment the palette to include an Primary Inverted option
declare module '@mui/material/styles' {
  interface Palette {
    primaryInverted: Palette['primary'];
  }

  interface PaletteOptions {
    primaryInverted?: PaletteOptions['primary'];
  }
}

declare module '@mui/material/AppBar' {
  interface AppBarPropsColorOverrides {
    primaryInverted: true;
  }
}

export function createAppTheme() {

  const base: ThemeOptions = {
    palette: {
      primary: {
        main: '#2b7bb9',
        contrastText: '#ffffff'
      },
      primaryInverted: {
        main: '#ffffff',
        contrastText: '#2b7bb9'
      },
      secondary: {
        main: '#0d47a1',
      },
      background: {
        default: '#f5f7fa',
        paper:  '#ffffff'
      }
    },
    typography: {
      fontFamily: ['Inter', 'Roboto', 'system-ui', 'Helvetica', 'Arial', 'sans-serif'].join(','),
      h1: { fontWeight: 700 },
      h2: { fontWeight: 600 }
    },
    components: {
      MuiCssBaseline: {
        styleOverrides: {
          // Use the theme background and, when provided, include the logo
          body: {
            backgroundColor: '#f5f7fa',
            color: '#0f1724',
            WebkitFontSmoothing: 'antialiased',
            MozOsxFontSmoothing: 'grayscale',
            // If a logo URL is provided, place it in the top-right as a subtle
            // branding watermark. Consumers can override this via GlobalStyles
            // or the theme component overrides if they prefer.
          },
          '*, *::before, *::after': {
            boxSizing: 'border-box',
          },
        },
      },
      MuiButton: {
        styleOverrides: {
          containedPrimary: {
            boxShadow: 'none',
            textTransform: 'none',
          },
        },
      },
    }
  }

  return createTheme(base)
}

// Default export: light theme without logo so existing imports work immediately.
export default createAppTheme()
