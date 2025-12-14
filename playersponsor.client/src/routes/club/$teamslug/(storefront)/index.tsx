import { createFileRoute } from '@tanstack/react-router'
import { Box, Container, Typography, Grid, Divider} from '@mui/material'
import HeroImageSection from '../../../../components/HeroImageSection'
import SponsorshipCard from '../../../../components/SponsorShipCard'
import SponsorShipPanel from '../../../../components/SponsorShipPanel';

export const Route = createFileRoute('/club/$teamslug/(storefront)/')({
   beforeLoad: () => {
    return {
      bar: true,
    }
  },
  loader: ({ context }) => {
    console.log('Loader context:', context);
    return context.bar // true
  },
  component: RouteComponent
})

function RouteComponent() {

const { teamslug } = Route.useParams()
const test =  Route.useLoaderData()
  
console.log('Sponsorships Page Loaded' + teamslug + ' ' + test);

  // --- Mock Data ---
  const MOCK_OPPORTUNITIES: SponsorshipOpportunity[] = [
    {
      id: '1',
      title: 'Man of the Match',
      price: '$500',
      description: 'Present the award to the MOTM and get a shoutout and photo on our social media.',
      category: 'Match Day',
      iconWord: 'trophy'
    },
    {
      id: '2',
      title: 'Match Ball Sponsor',
      price: '$250',
      description: 'Picture and mention on our social media as the official match ball sponsor.',
      category: 'Match Day',
      iconWord: 'gealicball'
    },
    {
      id: '3',
      title: 'Match Hydration Sponsor',
      price: '$2,500',
      description: 'Water or sports drinks for the team with your branding on the cooler.',
      category: 'Match Day',
      iconWord: 'hydration'
    },
    {
      id: '4',
      title: 'After Match Hydration Sponsor',
      price: '$2,500',
      description: 'Water or sports drinks for the team with your name on the cooler.',
      category: 'Match Day',
      iconWord: 'pints'
    },
    {
      id: '5',
      title: 'Player Sponsor',
      price: '$150',
      description: 'Dedicated post on Social Media annoucing your sponsorship of a player for the season.',
      category: 'Full Season',
      iconWord: 'playersponsor'
    }
  ];

  return (
    <Box sx={{ bgcolor: '#f5f5f5', minHeight: '100vh' }}>
      <Container maxWidth="lg" sx={{ pt: 4, pb: 8 }}>
        {/* Page Title */}
        <Typography variant="h3" component="h1"  gutterBottom align="left" sx={{ mt: 4, mb: 6, fontWeight: "bold" }}>
          Support Clan Na Gael
        </Typography>

        <HeroImageSection />

        {/* Sponsorship Products Section (3-column grid for desktop, 1-column for mobile) */}
        <Grid container spacing={4} sx={{ mb: 8 }}>
          <Box sx={{ bgcolor: '#f5f7fa', minHeight: '100vh' }}>
            {/* Main Content Area */}
            <Box sx={{ bgcolor: 'white', borderRadius: 4, p: 3, boxShadow: '0 4px 20px rgba(0,0,0,0.05)' }}>
              <Typography variant="h4" sx={{ mt: 2, mb: 3, fontWeight: "bold" }} component="h2" fontWeight="bold" color="text.primary">
                Featured Sponsorship Opportunities
              </Typography>
              <Grid container spacing={3}>
                {MOCK_OPPORTUNITIES.map((item) => (
                  <Grid size={{ xs: 12, sm: 6, md: 4 }} key={item.id}>
                    <SponsorshipCard item={item} />
                  </Grid>
                ))}
              </Grid >
            </Box>
          </Box >
        </Grid>

        {/* --- Contact Form Section --- */}
        <Divider sx={{ my: 4 }} />

        <Box sx={{ bgcolor: 'white', borderRadius: 4, p: 3 }} >
          <SponsorShipPanel email='odonnellpeter1999@gmail.com' socialLinks={ [{name: 'facebook', url: 'https://facebook.com/yourprofile'}, {name: 'instagram', url: 'https://facebook.com/yourprofile'}, {name: 'twitter', url: 'https://facebook.com/yourprofile'}]} />
        </Box>
      </Container>
    </Box>
  )
}
