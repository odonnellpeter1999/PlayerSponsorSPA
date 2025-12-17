import { createFileRoute } from '@tanstack/react-router'
import { Box, Container, Typography, Grid, Divider } from '@mui/material'
import HeroImageSection from '../../../../components/HeroImageSection'
import SponsorshipCard from '../../../../components/SponsorShipCard'
import ContactUsPanel from '../../../../components/ContactUsPanel';

export const Route = createFileRoute('/club/$teamslug/(storefront)/')({
  beforeLoad: () => {
    return {
      bar: true,
    }
  },
  loader: ({ context }) => {
    console.log('Loader context:', context);
    return context // true
  },
  component: RouteComponent
})

function RouteComponent() {

  const clubData = Route.useLoaderData()

  return (
    <Box sx={{ bgcolor: '#f5f5f5', minHeight: '100vh' }}>
      <Container  sx={{ pt: 4, pb: 8 }}>
        {/* Page Title */}
        <Typography variant="h3" component="h1" gutterBottom align="left" sx={{ mt: 4, mb: 6, fontWeight: "bold" }}>
          Support Clan Na Gael
        </Typography>

        <HeroImageSection />

        {/* Sponsorship Products Section (3-column grid for desktop, 1-column for mobile) */}
        <Grid sx={{ mb: 8 }}>
          <Box sx={{ bgcolor: '#f5f7fa', minHeight: '100vh' }}>
            {/* Main Content Area */}
            <Box sx={{ bgcolor: 'white', borderRadius: 4, p: 3, boxShadow: '0 4px 20px rgba(0,0,0,0.05)' }}>
              <Typography variant="h4" sx={{ mt: 2, mb: 3, fontWeight: "bold" }} component="h2" fontWeight="bold" color="text.primary">
                Featured Sponsorship Opportunities
              </Typography>
              <Grid container spacing={3}>
                {clubData.products.map((product) => (
                  <Grid size={{ xs: 12, sm: 6, md: 4 }} key={product.id}>
                    <SponsorshipCard product={product} />
                  </Grid>
                ))}
              </Grid >
            </Box>
          </Box >
        </Grid>

        {/* --- Contact Form Section --- */}
        <Divider sx={{ my: 4 }} />

        <Box sx={{ bgcolor: 'white', borderRadius: 4, p: 3 }} >
          <ContactUsPanel email='odonnellpeter1999@gmail.com' socialLinks={[{ name: 'facebook', url: 'https://facebook.com/yourprofile' }, { name: 'instagram', url: 'https://facebook.com/yourprofile' }, { name: 'twitter', url: 'https://facebook.com/yourprofile' }]} />
        </Box>
      </Container>
    </Box>
  )
}
