import { createFileRoute, Outlet } from '@tanstack/react-router'
import { createTheme, ThemeOptions, ThemeProvider } from '@mui/material/styles';

export const Route = createFileRoute('/club/$teamslug')({
  beforeLoad: ({params}) => {

    var clubDetails = loader(params.teamslug);

    return clubDetails;
  },
  loader: ({ context }) => {
    return context.club
  },
  component: RouteComponent
})

// TODO: Replace with real data fetching logic
const clubs = {
  clannageal: { name: 'Lions Club', location: 'New York', founded: 1905, primaryColor: '#348c34', secondaryColor: '#000000ff' },
  pats: { name: 'Tigers Club', location: 'Chicago', founded: 1920, primaryColor: '#d90b00', secondaryColor: '#0b0143' }
}

export const loader = async (teamslug:string) => {
  const club = clubs[teamslug];
  if (!club) {
    throw new Error(`Club not found for teamslug: ${teamslug}`);
  }

  return { club };
};

function RouteComponent() {

  const clubData = Route.useLoaderData();

  var base: ThemeOptions = {
    palette: {
      primary: {
        main: clubData.primaryColor,
        contrastText: '#ffffff'
      },
      primaryInverted: {
        main: '#ffffff',
        contrastText: '#ffffffff'
      },
      secondary: {
        main: clubData.secondaryColor,
      },
      background: {
        default: '#f5f7fa',
        paper: '#ffffff'
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

  return (
    <ThemeProvider theme={createTheme(base)}>
      <Outlet />
    </ThemeProvider>
  );
}
