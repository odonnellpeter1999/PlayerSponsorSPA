import { Box, Card, CardMedia } from '@mui/material';
import { cloudinary } from "../utils/images/cloudinary";

interface HeroImageSectionProps {
  imageId: string;
}

const HeroImageSection = ({ imageId }: HeroImageSectionProps) => {
  const heroImageUrl = cloudinary.getRawImageUrl(imageId);

  return (
    <Box sx={{ mb: 6, textAlign: 'center' }}>
      {/* The Hero Image Card */}
      <Card sx={{ maxWidth: '100%', borderRadius: 2, boxShadow: 3, mx: 'auto' }}>
        <CardMedia
          component="img"
          height="300" // Fixed height for a consistent look
          image={heroImageUrl} // Placeholder image
          alt="Club Stadium or Team"
          sx={{ objectFit: 'cover', width: '100%' }} // Ensure it covers the area and is responsive
        />
      </Card>
    </Box>
  );
};

export default HeroImageSection;