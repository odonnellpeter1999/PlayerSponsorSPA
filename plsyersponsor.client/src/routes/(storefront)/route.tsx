import { createFileRoute, Outlet } from '@tanstack/react-router'
import ResponsiveAppBar from '../../components/ResponsiveAppBar'
import AppBar from '@mui/material/AppBar'
import Container from '@mui/material/Container'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'

export const Route = createFileRoute('/(storefront)')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <>
      <ResponsiveAppBar />
      <Outlet />
      <AppBar position="static" color="primary" component="footer" sx={{ top: 'auto', bottom: 0, mt: 4 }}>
        <Container maxWidth="lg">
          <Toolbar disableGutters sx={{ justifyContent: 'center', py: 2 }}>
            <Typography variant="body2" color="inherit">
              � {new Date().getFullYear()} My Simple MUI Site
            </Typography>
          </Toolbar>
        </Container>
      </AppBar>
    </>
  )
}


