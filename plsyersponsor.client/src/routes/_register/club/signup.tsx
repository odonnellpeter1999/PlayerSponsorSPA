import { createFileRoute } from '@tanstack/react-router'
import { useState } from 'react';
import { Box, Paper, Typography, TextField, Button, Alert, Divider, Container, Grid } from '@mui/material';
import { CreditCard, WarningAmber, Mail, Phone, CheckCircle } from '@mui/icons-material';
import { useCreateUser } from '../../../queries/users/mutations';
import { CreateClubRequest } from '../../../queries/users/types';

const SignUp = () => {
    const createClubMutation = useCreateUser();

    // State for all administrative details
    const [clubName, setClubName] = useState('Test');
    const [description, setDescription] = useState('Testing');
    const [publicContactEmail, setPublicContactEmail] = useState('test2@gmail.com');
    const [adminPhone, setAdminPhone] = useState('0838376683');
    const [password, setPassword] = useState('Testauto123!');
    const [confirmPassword, setConfirmPassword] = useState('Testauto123!');
    const [eTransferEmail, setETransferEmail] = useState('odonnellpeter1999@gmail.com');

    const [isSaving, setIsSaving] = useState(false);
    const [saveStatus, setSaveStatus] = useState(null); // 'success' or 'error'

    const handleSave = (e) => {
        e.preventDefault();
        setIsSaving(true);
        setSaveStatus(null);

        const request:CreateClubRequest = {
            adminAccountDetails: {
                email: publicContactEmail,
                password: password,
                confirmPassword: confirmPassword,
                phoneNumber: adminPhone
            },
            clubDetails: {
                name: clubName,
                description: description,
                interacEmail: eTransferEmail
            }
        };
        
        var response = createClubMutation.mutateAsync(request);
    };

    return (
        <Container maxWidth="md" sx={{ py: 4, minHeight: "100vh", backgroundColor: "#78a2cdff" }}>
            <Paper elevation={4} sx={{ borderRadius: '8px' }}>
                <Box sx={{ p: 2, display: 'flex', justifyContent: 'center', alignItems: 'center', bgcolor: 'transparent', borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }}>
                    <Box
                        component="img"
                        src={'/playersponsor-logo.png'}
                        alt="PlayerSponsor logo"
                        sx={{ height: 80, objectFit: 'contain' }}
                    />
                </Box>
                <Divider />
                {/* Header (Simulating AppBar) */}
                <Grid container justifyContent={'center'} sx={{ p: 3, borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }} >
                    <Typography variant="h4" component="h1" sx={{ fontWeight: 700, display: "flex", alignItems: "center", gap: 1 }}>
                        Create Your Club Account
                    </Typography>
                </Grid>

                {/* Status Message (Simulating Alert) */}
                {saveStatus === 'success' && (
                    <Alert icon={<CheckCircle fontSize="inherit" />} severity="success" sx={{ borderRadius: 0 }}>
                        Details saved successfully and published!
                    </Alert>
                )}
                {saveStatus === 'error' && (
                    <Alert icon={<WarningAmber fontSize="inherit" />} severity="error" sx={{ borderRadius: 0 }}>
                        Error saving details. Please check the form and try again.
                    </Alert>
                )}

                <Box component="form" onSubmit={handleSave} sx={{ p: { xs: 3, sm: 4 }, display: "flex", flexDirection: "column", gap: 5 }}>
                    {/* Section 1: Public Identity */}
                    <Box>
                        <Typography variant="h5" component="h2" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee" }}>
                            Public Club Details
                        </Typography>

                        <TextField
                            label="Club Name (Title)"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            value={clubName}
                            onChange={(e) => setClubName(e.target.value)}
                            required
                        />

                        <TextField
                            label="Description (Public Text)"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            multiline
                            rows={4}
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            helperText="This text appears prominently on your public support page."
                            required
                        />
                    </Box>
                    <Divider />
                    {/* Section 2: Administrative Account Information */}
                    <Box>
                        <Typography variant="h5" component="h2" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee" }}>
                            Administrative Account Information
                        </Typography>

                        <TextField
                            label="Public Contact Email"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            type="email"
                            value={publicContactEmail}
                            onChange={(e) => setPublicContactEmail(e.target.value)}
                            InputProps={{ startAdornment: <Mail sx={{ mr: 1, color: 'action.active' }} /> }}
                            required
                        />

                        <TextField
                            label="Admin Contact Phone (Optional)"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            type="tel"
                            value={adminPhone}
                            onChange={(e) => setAdminPhone(e.target.value)}
                            InputProps={{ startAdornment: <Phone sx={{ mr: 1, color: 'action.active' }} /> }}
                        />

                        <TextField
                            label="Password"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />

                        <TextField
                            label="Confirm Password"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            type="password"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                        />
                    </Box>

                    <Divider />

                    {/* Section 3: Payment Configuration */}
                    <Box>
                        <Typography variant="h5" component="h2" color="error" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee", display: "flex", alignItems: "center", gap: 1 }}>
                            <CreditCard />
                            Interac e-Transfer Configuration
                        </Typography>

                        <Alert severity="warning" sx={{ mb: 2 }}>
                            <Typography variant="subtitle2" sx={{ fontWeight: 700 }}>CRITICAL PAYMENT SETTING:</Typography>
                            This is the exact email linked to your bank account for **Autodeposit**. Do not share this publicly for inquiries.
                        </Alert>

                        <TextField
                            label="E-Transfer Recipient Email"
                            fullWidth
                            margin="normal"
                            variant="outlined"
                            type="email"
                            value={eTransferEmail}
                            onChange={(e) => setETransferEmail(e.target.value)}
                            sx={{ '& .MuiOutlinedInput-root': { borderColor: 'error.main' } }}
                            required
                        />
                    </Box>

                    {/* Action Button */}
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        size="large"
                        disabled={isSaving}
                        sx={{ mt: 2, py: 1.5, borderRadius: '24px', fontWeight: 600, fontSize: '1.1rem' }}
                    >
                        {isSaving ? 'Saving...' : 'Next'}
                    </Button>
                </Box>
            </Paper>
        </Container>
    );
};

export const Route = createFileRoute('/_register/club/signup')({
    component: SignUp,
})