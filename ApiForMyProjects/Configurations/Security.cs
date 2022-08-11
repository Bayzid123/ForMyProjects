public static class Security
{
    public static void AddHeaderPolicy(this IApplicationBuilder app)
    {
        // Security Header
        var policyCollection = new HeaderPolicyCollection()
            .AddXssProtectionBlock()
            .AddContentTypeOptionsNoSniff()
            .AddExpectCTNoEnforceOrReport(0)
            .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
            .AddReferrerPolicyStrictOriginWhenCrossOrigin()
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddUpgradeInsecureRequests();
                builder.AddDefaultSrc().Self();
                builder.AddConnectSrc().From("*");
                builder.AddFontSrc().From("*");
                builder.AddFrameAncestors().From("*");
                builder.AddFrameSource().From("*");
                builder.AddWorkerSrc().From("*");
                builder.AddMediaSrc().From("*");
                builder.AddImgSrc().From("https://erp.ibos.io").Data();
                builder.AddObjectSrc().From("*");
                builder.AddScriptSrc().From("*").UnsafeInline().UnsafeEval();
                builder.AddStyleSrc().From("*").UnsafeEval().UnsafeInline();
            })
            .RemoveServerHeader();

        app.UseSecurityHeaders(policyCollection);
    }
}