import { Box, Button, Card, CardActions, CardContent, Chip, Typography } from "@mui/material";
import { SponsorshipOpportunity } from "./Carousel";
import SponsorShipIcon from "./SponsorShipIcon";

const SponsorshipCard = ({ item }: { item: SponsorshipOpportunity }) => {
  return (
    <Card
      elevation={3}
      sx={{
        height: '100%',
        display: 'flex',
        flexDirection: 'column',
        borderRadius: 4,
        transition: 'transform 0.2s',
        '&:hover': {
          transform: 'translateY(-5px)',
          boxShadow: 6
        }
      }}
    >
      <Box
        sx={{
          p: 4,
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          bgcolor: 'primary.light',
          color: 'primary.contrastText'
        }}
      >
      <SponsorShipIcon word={item.iconWord} />
      </Box>
      <CardContent sx={{ flexGrow: 1, textAlign: 'center' }}>
        <Chip
          label={item.category}
          size="small"
          color="primary"
          variant="outlined"
          sx={{ mb: 2 }}
        />
        <Typography gutterBottom variant="h6" component="h3" fontWeight="bold">
          {item.title}
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
          {item.description}
        </Typography>
        <Typography variant="h5" color="primary.main" fontWeight="800">
          {item.price}
        </Typography>
      </CardContent>
      <CardActions sx={{ p: 2, pt: 0, justifyContent: 'center' }}>
        <Button variant="contained" color="secondary" fullWidth disableElevation sx={{ borderRadius: 2 }}>
          Sponsor Now
        </Button>
      </CardActions>
    </Card>
  );
};

export default SponsorshipCard;