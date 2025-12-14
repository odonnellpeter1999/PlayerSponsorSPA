import { createFileRoute } from '@tanstack/react-router'
import {
    Typography,
    Button,
    Container,
    Grid,
    Card,
    CardContent,
    CardActions,
} from '@mui/material';

export const Route = createFileRoute('/(public)/')({
    component: HomePage,
})


// The main homepage component
function HomePage() {
    return (
        <>
            {/* 2. Main Content Container */}
            <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                {/* Hero Section */}
                <Typography variant="h2" component="h1" gutterBottom align="center">
                    Welcome to Our Homepage!
                </Typography>
                <Typography variant="h5" align="center" color="text.secondary" paragraph>
                    This is a responsive layout built easily with Material UI components.
                </Typography>
                <Grid container spacing={2} justifyContent="center" sx={{ mb: 6 }}>
                    <Grid>
                        <Button variant="contained" color='secondary' size="large">
                            Get Started
                        </Button>
                    </Grid>
                    <Grid>
                        <Button variant="outlined" color='secondary' size="large">
                            Learn More
                        </Button>
                    </Grid>
                </Grid>

                {/* Feature Cards Section using Grid */}
                <Grid container spacing={4}>
                    {/* Feature Card 1 */}
                    <Grid size={{xs:12, sm:6, md:4}}>
                        <Card raised>
                            <CardContent>
                                <Typography variant="h5" component="div">
                                    Feature One
                                </Typography>
                                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                                    Innovative
                                </Typography>
                                <Typography variant="body2">
                                    Use the Card component to neatly group related content and actions.
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button size="small">View Details</Button>
                            </CardActions>
                        </Card>
                    </Grid>

                    {/* Feature Card 2 */}
                    <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                        <Card raised>
                            <CardContent>
                                <Typography variant="h5" component="div">
                                    Feature Two
                                </Typography>
                                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                                    Fast
                                </Typography>
                                <Typography variant="body2">
                                    The Grid component makes responsive layout design simple and effective.
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button size="small">Explore</Button>
                            </CardActions>
                        </Card>
                    </Grid>

                    {/* Feature Card 3 */}
                    <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                        <Card raised>
                            <CardContent>
                                <Typography variant="h5" component="div">
                                    Feature Three
                                </Typography>
                                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                                    Accessible
                                </Typography>
                                <Typography variant="body2">
                                    Material UI is built with accessibility in mind right out of the box.
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button size="small">Read More</Button>
                            </CardActions>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}