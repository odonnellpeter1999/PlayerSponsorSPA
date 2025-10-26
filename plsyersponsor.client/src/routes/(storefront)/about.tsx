import { createFileRoute } from '@tanstack/react-router'
import { Container, Typography, Paper, Box, Button } from '@mui/material'

export const Route = createFileRoute('/(storefront)/about')({
    component: About,
})

function About() {
    return (
        <Container maxWidth="md" sx={{ my: 4 }}>
            <Paper elevation={2} sx={{ p: 3 }}>
                <Typography variant="h4" component="h1" gutterBottom>
                    About
                </Typography>

                <Typography variant="body1" paragraph>
                    This is a simple, clean About page built with MUI. It demonstrates a
                    consistent layout and styling that fits into the app's root layout.
                </Typography>

                <Typography variant="body2" color="text.secondary" paragraph>
                    Use this page to describe your project, link to documentation, or add
                    contact information. Keep content concise and focused for the best
                    user experience.
                </Typography>

                <Box sx={{ display: 'flex', gap: 2, mt: 2 }}>
                    <Button variant="contained" href="/">
                        Back to Home
                    </Button>
                    <Button variant="outlined" href="#contact">
                        Contact
                    </Button>
                </Box>
            </Paper>
        </Container>
    )
}