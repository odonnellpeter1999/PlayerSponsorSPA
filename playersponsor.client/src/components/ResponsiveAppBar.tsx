import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import { Link } from '@tanstack/react-router';
import Logo from './LogoNavBar';
import { useTheme } from '@mui/material/styles';

interface ResponsiveAppBarProps {
  links: { label: string; href: string }[];
}

const ResponsiveAppBar = ({ links }: ResponsiveAppBarProps) => {

  const [openNavDrawer, setOpenNavDrawer] = React.useState(false);

  const theme = useTheme();

  const handleOpenNavMenu = (_event: React.MouseEvent<HTMLElement>) => {
    setOpenNavDrawer(true);
  };

  const handleCloseNavMenu = () => {
    setOpenNavDrawer(false);
  };

  return (
    <AppBar position="static" sx={{ bgcolor: 'white', borderBottom: 3, borderColor: theme.palette.secondary.main }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Logo shieldBorderColour='black' shieldBorderFill={theme.palette.primary.main} />
          <Typography variant="h4" sx={{ color: theme.palette.primary.main, fontWeight: 'bold', ml: 2 }}>
            PlayerSponsor
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' }, justifyContent: 'flex-end' }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="nav-drawer"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="primary"
            >
              <MenuIcon />
            </IconButton>
            <Drawer
              id="nav-drawer"
              anchor="left"
              open={openNavDrawer}
              onClose={handleCloseNavMenu}
            >
              <Box sx={{ width: 250 }} role="presentation" onClick={handleCloseNavMenu} onKeyDown={handleCloseNavMenu}>
                <List>
                  {links.map((link) => (
                    <ListItem disablePadding>
                      <ListItemButton>
                        <Link to={link.href} style={{ width: '100%', textDecoration: 'none', color: theme.palette.text.primary }}>
                          <ListItemText primary={link.label.toUpperCase()} color='secondary' />
                        </Link>
                      </ListItemButton>
                    </ListItem>
                  ))}
                </List>
              </Box>
            </Drawer>
          </Box>
          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' }, justifyContent: 'flex-end' }}>
            {links.map((link) => (
              <Button
                key={link.label}
                href={link.href}
                color='secondary'
                sx={{ my: 2, display: 'block' }}
              >
                {link.label}
              </Button>
            ))}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default ResponsiveAppBar;
