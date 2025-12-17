import { Box, Button, Card, CardActions, CardContent, Chip, Typography } from "@mui/material";
import SponsorShipIcon from "./SponsorShipIcon";
import { Product } from "../types/club";

const SponsorshipCard = ({ product }: { product: Product }) => {

  console.log(product);

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
        <SponsorShipIcon word={product?.iconWord} />
      </Box>
      <CardContent sx={{ flexGrow: 1, textAlign: 'center' }}>
        {product?.tags && product.tags.map(tag => (
          <Chip
            key={tag}
            label={tag}
            size="small"
            color="primary"
            variant="outlined"
            sx={{ mb: 2, mx: 0.5 }}
          />
        ))}
        <Typography gutterBottom variant="h6" component="h3" fontWeight="bold">
          {product?.title}
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
          {product?.description}
        </Typography>
        <Typography variant="h5" color="primary.main" fontWeight="800">
          {product?.price}
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