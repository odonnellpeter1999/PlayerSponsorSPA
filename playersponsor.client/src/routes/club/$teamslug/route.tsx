import { createFileRoute, Outlet } from '@tanstack/react-router'
import { ThemeProvider } from '@mui/material/styles';
import { Club, sampleClub } from '../../../types/club';
import { createClubTheme } from '../../../theme';

export const Route = createFileRoute('/club/$teamslug')({
  beforeLoad: ({params}) => {

    var clubDetails = loader(params.teamslug);

    return clubDetails;
  },
  loader: ({ context }) => {
    return context
  },

  component: RouteComponent
})

export const loader = async (teamslug:string):Promise<Club> => {
  // In a real application, you would fetch club data based on the teamslug.
  return sampleClub;
};

function RouteComponent() {

  const clubData = Route.useLoaderData();

  return (
    <ThemeProvider theme={createClubTheme(clubData.primaryColor, clubData.secondaryColor)}>
      <Outlet />
    </ThemeProvider>
  );
}
