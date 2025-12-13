import { Box, Fade, Grid, IconButton, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useEffect, useState } from "react";
import SponsorshipCard from "./SponsorShipCard";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';


export interface SponsorshipOpportunity {
  id: string;
  title: string;
  price: string;
  description: string;
  iconWord: string;
  category: 'Match Day' | 'Kit' | 'Digital' | 'Facilities'; // Move to ID
}

interface CarouselProps {
  items: SponsorshipOpportunity[];
  title: string;
}

const Carousel = ({ items, title }: CarouselProps) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const isTablet = useMediaQuery(theme.breakpoints.between('sm', 'md'));

  // Determine items per page based on screen size
  const itemsPerPage = isMobile ? 1 : isTablet ? 2 : 3;
  
  const [activeStep, setActiveStep] = useState(0);

  // Reset activeStep when itemsPerPage changes
  useEffect(() => {
    setActiveStep(0);
  }, [itemsPerPage]);
  
  const maxSteps = Math.ceil(items.length / itemsPerPage);

  const handleNext = () => {
    setActiveStep((prevStep) => (prevStep + 1) % maxSteps);
  };

  const handleBack = () => {
    setActiveStep((prevStep) => (prevStep - 1 + maxSteps) % maxSteps);
  };

  // Calculate the items to display for the current step
  const startIndex = activeStep * itemsPerPage;
  const currentItems = items.slice(startIndex, startIndex + itemsPerPage);

  return (
    <Box sx={{ width: '100%', py: 4, position: 'relative' }}>
      {/* Header Row */}
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3, px: 2 }}>
        <Typography variant="h5" component="h2" fontWeight="bold" color="text.primary">
          {title}
        </Typography>
        
        {/* Navigation Arrows */}
        <Box>
          <IconButton 
            onClick={handleBack} 
            color="primary" 
            disabled={maxSteps <= 1}
            sx={{ border: '1px solid', borderColor: 'divider', mr: 1 }}>
            <ArrowBackIcon />
          </IconButton>
          <IconButton 
            onClick={handleNext} 
            color="primary"
            disabled={maxSteps <= 1} 
            sx={{ border: '1px solid', borderColor: 'divider' }}>
            <ArrowForwardIcon />
          </IconButton>
        </Box>
      </Box>

      {/* Carousel Content */}
      <Box sx={{ overflow: 'hidden', paddingTop: 1, minHeight: 400 }}>
        {/* We use a Fade transition here for smooth switching between chunks of cards */}
        <Fade in={true} key={activeStep} timeout={500}>
          <Box>
             <Grid container spacing={3} sx={{ px: 2 }}>
              {currentItems.map((item) => (
                <Grid size={{xs:12, sm:6, md:4}} key={item.id}>
                  <SponsorshipCard item={item} />
                </Grid>
              ))}
              
              {/* Fillers to keep layout stable if last page has fewer items */}
              {currentItems.length < itemsPerPage && 
                Array.from(new Array(itemsPerPage - currentItems.length)).map((_, index) => (
                   <Grid size={{xs:12, sm:6, md:4}} key={`placeholder-${index}`} sx={{visibility: 'hidden'}}>
                      <Box sx={{ height: '100%' }} />
                   </Grid>
                ))
              }
            </Grid>
          </Box>
        </Fade>
      </Box>

      {/* Pagination Dots */}
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
        {Array.from(new Array(maxSteps)).map((_, index) => (
          <Box
            key={index}
            onClick={() => setActiveStep(index)}
            sx={{
              width: 10,
              height: 10,
              borderRadius: '50%',
              bgcolor: index === activeStep ? 'primary.main' : 'action.disabled',
              mx: 0.5,
              cursor: 'pointer',
              transition: 'background-color 0.3s'
            }}
          />
        ))}
      </Box>
    </Box>
  );
};

export default Carousel;