import React from 'react';
import { SvgIconProps } from '@mui/material';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import SportsVolleyballIcon from '@mui/icons-material/SportsVolleyball';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import SportsBarIcon from '@mui/icons-material/SportsBar';
import SportsBaseballIcon from '@mui/icons-material/SportsBaseball';
import LocalDrinkIcon from '@mui/icons-material/LocalDrink';
import DirectionsRunIcon from '@mui/icons-material/DirectionsRun';

interface SponsorShipIconProps {
  word: string;
}

const SponsorShipIcon: React.FC<SponsorShipIconProps> = ({ word }) => {
  const iconMap: Record<string, React.ElementType<SvgIconProps>> = {
    soccerball: SportsSoccerIcon,
    gealicball: SportsVolleyballIcon,
    trophy: EmojiEventsIcon,
    pint: SportsBarIcon,
    sliotar: SportsBaseballIcon,
    hydration: LocalDrinkIcon,
    pints: SportsBarIcon,
    playersponsor: DirectionsRunIcon,
  };

  const IconComponent = iconMap[word.toLowerCase()] || ErrorOutlineIcon;

  return <IconComponent fontSize="large" />;
};

export default SponsorShipIcon;