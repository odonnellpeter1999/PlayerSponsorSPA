import {
  AppBar,
  Toolbar,
  Typography,
  Container,
  Box,
  Grid,
  Card,
  CardMedia,
  CardContent,
  CardActions,
  Button,
  TextField,
  Divider,
} from '@mui/material';
import HeroImageSection from './HeroImageSection';

const SponsorshipPageSimplified = () => {
  const sponsorshipProducts = [
    {
      id: 1,
      name: 'Game Day Jersey Sponsor',
      image: 'https://via.placeholder.com/600x300?text=Jersey+Sponsorship+Image',
      description: 'Your logo prominently displayed on our home game day jerseys for the entire season. A high-visibility option!',
    },
    {
      id: 2,
      name: 'Training Equipment',
      image: 'https://via.placeholder.com/600x300?text=Training+Equipment+Image',
      description: 'Help us gear up! Your brand on all training equipment, seen at practices and youth clinics.',
    },
    {
      id: 3,
      name: 'Match Ball Sponsor',
      image: 'https://via.placeholder.com/600x300?text=Match+Ball+Image',
      description: 'Be us name behind on the game! logo on all official match balls used competitive play.',
    },
  ];

  return (
    <Box sx={{ bgcolor: '#f5f5f5', minHeight: '100vh' }}>
      <Container maxWidth="lg" sx={{ pt: 4, pb: 8 }}>
        
        {/* Page Title */}
        <Typography variant="h3" component="h1" gutterBottom align="left" sx={{ mt: 4, mb: 6, fontWeight: 'bold' }}>
          Support Clan Na Gael
        </Typography>

        <HeroImageSection />

        {/* Sponsorship Products Section (3-column grid for desktop, 1-column for mobile) */}
        <Grid container spacing={4} sx={{ mb: 8 }}>
          {sponsorshipProducts.map((product) => (
            <Grid key={product.id} size={{xs:12, sm:6, md:4}}>
              <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column', boxShadow: 3, borderRadius: 2 }}>
                <CardMedia
                  component="img"
                  height="180"
                  image={product.image}
                  alt={product.name}
                  sx={{ objectFit: 'cover' }}
                />
                <CardContent sx={{ flexGrow: 1 }}>
                  <Typography gutterBottom variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                    {product.name}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {product.description}
                  </Typography>
                </CardContent>
                <CardActions sx={{ p: 2 }}>
                  <Button size="large" variant="contained" color="primary" fullWidth>
                    Sponsor Now
                  </Button>
                </CardActions>
              </Card>
            </Grid>
          ))}
        </Grid>
        
        {/* --- Contact Form Section --- */}
        <Divider sx={{ my: 4 }} />
        
        <Box sx={{ textAlign: 'center', mb: 4 }}>
          <Typography variant="h4" component="h2" gutterBottom sx={{ fontWeight: 'bold' }}>
            Have a Custom Request?
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Reach out to discuss tailored sponsorship packages.
          </Typography>
        </Box>

        <Container maxWidth="sm" sx={{ mb: 8, p: { xs: 2, md: 4 }, bgcolor: 'background.paper', borderRadius: 2, boxShadow: 3 }}>
          <Grid container spacing={3}>
            <Grid size={{xs:12}}>
              <TextField fullWidth label="Your Name" variant="outlined" />
            </Grid>
            <Grid size={{xs:12}}>
              <TextField fullWidth label="Your Email" variant="outlined" type="email" />
            </Grid>
            <Grid size={{xs:12}}>
              <TextField
                fullWidth
                label="Your Message"
                variant="outlined"
                multiline
                rows={4}
              />
            </Grid>
            <Grid size={{xs:12}}>
              <Button fullWidth variant="contained" color="secondary" size="large">
                Send Message
              </Button>
            </Grid>
          </Grid>
        </Container>

      </Container>
    </Box>
  );
};

export default SponsorshipPageSimplified;