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
import AdbIcon from '@mui/icons-material/Adb';
import { Link } from '@tanstack/react-router';
import Logo from './LogoNavBar';
import theme from '../theme';

const pages = ['Products', 'Pricing', 'Blog'];

function ResponsiveAppBar() {
  const [openNavDrawer, setOpenNavDrawer] = React.useState(false);

  const handleOpenNavMenu = (_event: React.MouseEvent<HTMLElement>) => {
    setOpenNavDrawer(true);
  };

  const handleCloseNavMenu = () => {
    setOpenNavDrawer(false);
  };

  return (
    <AppBar position="static" sx={{ bgcolor: 'white', borderBottom: 3, borderColor: theme.palette.primary.main }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Logo shieldBorderColour={theme.palette.primary.main} shieldBorderFill='white'  />
          <Typography variant="h4" sx={{ color: theme.palette.primary.main, fontWeight: 'bold', ml: 2 }}>
            Player Sponsor
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
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
                  <ListItem disablePadding>
                    <Link to="/about" >
                      <ListItemButton>
                        <ListItemText primary="about" />
                      </ListItemButton>
                    </Link>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton>
                      <ListItemText primary="about" />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton>
                      <ListItemText primary="about" />
                    </ListItemButton>
                  </ListItem>
                </List>
              </Box>
            </Drawer>
          </Box>
          <AdbIcon sx={{ display: { xs: 'flex', md: 'none' }, mr: 1 }} />
          <Typography
            variant="h5"
            noWrap
            component="a"
            href="#app-bar-with-responsive-menu"
            sx={{
              mr: 2,
              display: { xs: 'flex', md: 'none' },
              flexGrow: 1,
              fontFamily: 'monospace',
              fontWeight: 700,
              letterSpacing: '.3rem',
              color: 'inherit',
              textDecoration: 'none',
            }}
          >
            LOGO
          </Typography>
            <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' }, justifyContent: 'flex-end' }}>
            {pages.map((page) => (
              <Button
              key={page}
              sx={{ my: 2, display: 'block' }}
              >
              {page}
              </Button>
            ))}
            </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default ResponsiveAppBar;
