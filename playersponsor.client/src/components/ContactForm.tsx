import { Box, Button, Grid, TextField, Typography } from '@mui/material';

const ContactForm = () => {
  return (
    <Box sx={{ bgcolor: 'white', borderRadius: 4, p: 3 }} >
      <Box sx={{ textAlign: 'center', mb: 4 }}>
        <Typography variant="h4" component="h2" gutterBottom sx={{ fontWeight: "bold" }}>
          Have a Custom Request?
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Reach out to discuss tailored sponsorship packages.
        </Typography>
      </Box>

      <Grid container spacing={3}>
        <Grid size={{ xs: 12 }}>
          <TextField fullWidth label="Your Name" variant="outlined" />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <TextField fullWidth label="Your Email" variant="outlined" type="email" />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <TextField87
            fullWidth
            label="Your Message"
            variant="outlined"
            multiline
            rows={4}
          />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <Button fullWidth variant="contained" size="large">
            Send Message
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default ContactForm;