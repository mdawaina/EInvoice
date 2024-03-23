using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XmlGenerator
{

    public partial class frmXmlGenerator : Form
    {
        public frmXmlGenerator()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //String itemDescription = txtItemName.Text;
            //decimal quantity = Convert.ToDecimal(txtQty.Text);
            //decimal price = Convert.ToDecimal(txtPrice.Text);
            //decimal vatRate = Convert.ToDecimal(txtVatRate.Text);


            // Input data
            //string itemDescription = "sweets";
            //decimal quantity = 1;
            // decimal price = 10;
            // decimal vatRate = 0.15m;

            // Create the XML document
            /* XDocument xmlDocument = new XDocument(
                 new XElement("Invoice",
                     new XElement("ItemDescription", itemDescription),
                     new XElement("Quantity", quantity),
                     new XElement("Price", price),
                     new XElement("TaxCurrencyCode", taxCurrencyCode),
                     new XElement("VAT", CalculateVAT(price, vatRate))
                 )
             );*/
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Please input item name.", "Error");
                return;
            }
            if (txtQty.Text == "")
            {
                MessageBox.Show("Please input quentity.", "Error");
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Please input price amount.", "Error");
                return;
            }
            if (txtUnit.Text == "")
            {
                MessageBox.Show("Please input currency code.", "Error");
                return;
            }
            if (txtVatRate.Text == "")
            {
                MessageBox.Show("Please input VAT rate.", "Error");
                return;
            }

            string invoiceLineID = "1";
            decimal invoicedQuantity = Convert.ToDecimal(txtQty.Text);
            string itemName = txtItemName.Text;
            decimal taxPercentage = Convert.ToDecimal(txtVatRate.Text);
            decimal priceAmount = Convert.ToDecimal(txtPrice.Text);
            string taxCurrencyCode = txtUnit.Text;

            decimal lineExtensionAmount = 4.00m;
            decimal roundingAmount = 4.60m;
            string taxCategoryID = "S"; 
            string taxSchemeID = "VAT";
            

            decimal taxAmount = priceAmount * taxPercentage;
            bool chargeIndicator = true;
            string allowanceChargeReason = "discount";
            decimal allowanceChargeAmount = 0.00m;

            
            

            // Create the XML document
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(declaration);

            // Create the root element
            XmlElement rootElement = document.CreateElement("Invoice");
            rootElement.SetAttribute("xmlns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
            rootElement.SetAttribute("xmlns:cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.SetAttribute("xmlns:cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            rootElement.SetAttribute("xmlns:ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            document.AppendChild(rootElement);

            // Create the <ext:UBLExtensions> element
            XmlElement ublExtensionsElement = document.CreateElement("ext", "UBLExtensions", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            rootElement.AppendChild(ublExtensionsElement);

            // Create the <ext:UBLExtension> element
            XmlElement ublExtensionElement = document.CreateElement("ext", "UBLExtension", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            ublExtensionsElement.AppendChild(ublExtensionElement);

            // Create the <ext:ExtensionURI> element
            XmlElement extensionUriElement = document.CreateElement("ext", "ExtensionURI", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            extensionUriElement.InnerText = "urn:oasis:names:specification:ubl:dsig:enveloped:xades";
            ublExtensionElement.AppendChild(extensionUriElement);

            // Create the <ext:ExtensionContent> element
            XmlElement extensionContentElement = document.CreateElement("ext", "ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            ublExtensionElement.AppendChild(extensionContentElement);

            // Create the <sig:UBLDocumentSignatures> element
            XmlElement ublDocumentSignaturesElement = document.CreateElement("sig", "UBLDocumentSignatures", "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2");
            extensionContentElement.AppendChild(ublDocumentSignaturesElement);

            // Create the <sac:SignatureInformation> element
            XmlElement signatureInformationElement = document.CreateElement("sac", "SignatureInformation", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2");
            ublDocumentSignaturesElement.AppendChild(signatureInformationElement);

            // Create the <cbc:ID> element
            XmlElement idElement = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement.InnerText = "urn:oasis:names:specification:ubl:signature:1";
            signatureInformationElement.AppendChild(idElement);

            // Create the <sbc:ReferencedSignatureID> element
            XmlElement referencedSignatureIdElement = document.CreateElement("sbc", "ReferencedSignatureID", "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2");
            referencedSignatureIdElement.InnerText = "urn:oasis:names:specification:ubl:signature:Invoice";
            signatureInformationElement.AppendChild(referencedSignatureIdElement);

            // Create the <ds:Signature> element
            XmlElement signatureElement = document.CreateElement("ds", "Signature", "http://www.w3.org/2000/09/xmldsig#");
            signatureElement.SetAttribute("Id", "signature");
            signatureInformationElement.AppendChild(signatureElement);

            // Create the <ds:SignedInfo> element
            XmlElement signedInfoElement = document.CreateElement("ds", "SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
            signatureElement.AppendChild(signedInfoElement);

            // Create the <ds:CanonicalizationMethod> element
            XmlElement canonicalizationMethodElement = document.CreateElement("ds", "CanonicalizationMethod", "http://www.w3.org/2000/09/xmldsig#");
            canonicalizationMethodElement.SetAttribute("Algorithm", "http://www.w3.org/2006/12/xml-c14n11");
            signedInfoElement.AppendChild(canonicalizationMethodElement);

            // Create the <ds:SignatureMethod> element
            XmlElement signatureMethodElement = document.CreateElement("ds", "SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
            signatureMethodElement.SetAttribute("Algorithm", "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256");
            signedInfoElement.AppendChild(signatureMethodElement);

            // Create the <ds:Reference> element
            XmlElement referenceElement = document.CreateElement("ds", "Reference", "http://www.w3.org/2000/09/xmldsig#");
            referenceElement.SetAttribute("URI", "#Invoice");
            signedInfoElement.AppendChild(referenceElement);

            // Create the <ds:DigestMethod> element
            XmlElement digestMethodElement = document.CreateElement("ds", "DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
            digestMethodElement.SetAttribute("Algorithm", "http://www.w3.org/2001/04/xmlenc#sha256");
            referenceElement.AppendChild(digestMethodElement);

            // Create the <ds:DigestValue> element
            XmlElement digestValueElement = document.CreateElement("ds", "DigestValue", "http://www.w3.org/2000/09/xmldsig#");
            digestValueElement.InnerText = "DigestValueContent";
            referenceElement.AppendChild(digestValueElement);

            ///////////////////////////////////////////////////////////////////////////////

            // Add the ProfileID element
            XmlElement profileIdElement = document.CreateElement("cbc", "ProfileID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            profileIdElement.InnerText = "reporting:1.0";
            rootElement.AppendChild(profileIdElement);

            // Add the ID element
            XmlElement idElement2 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement2.InnerText = "SME00023";
            rootElement.AppendChild(idElement2);

            // Add the UUID element
            XmlElement uuidElement = document.CreateElement("cbc", "UUID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            uuidElement.InnerText = "8d487816-70b8-4ade-a618-9d620b73814a";
            rootElement.AppendChild(uuidElement);

            // Add the IssueDate element
            XmlElement issueDateElement = document.CreateElement("cbc", "IssueDate", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            issueDateElement.InnerText = "2022-09-07";
            rootElement.AppendChild(issueDateElement);

            // Add the IssueTime element
            XmlElement issueTimeElement = document.CreateElement("cbc", "IssueTime", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            issueTimeElement.InnerText = "12:21:28";
            rootElement.AppendChild(issueTimeElement);

            // Add the InvoiceTypeCode element
            XmlElement invoiceTypeCodeElement = document.CreateElement("cbc", "InvoiceTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            invoiceTypeCodeElement.InnerText = "388";
            invoiceTypeCodeElement.SetAttribute("name", "0100000");
            rootElement.AppendChild(invoiceTypeCodeElement);

            // Add the DocumentCurrencyCode element
            XmlElement documentCurrencyCodeElement = document.CreateElement("cbc", "DocumentCurrencyCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            documentCurrencyCodeElement.InnerText = "SAR";
            rootElement.AppendChild(documentCurrencyCodeElement);

            // Add the TaxCurrencyCode element
            XmlElement taxCurrencyCodeElement = document.CreateElement("cbc", "TaxCurrencyCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            taxCurrencyCodeElement.InnerText = "SAR";
            rootElement.AppendChild(taxCurrencyCodeElement);

            // Add the AdditionalDocumentReference elements
            XmlElement additionalDocumentReferenceElement1 = document.CreateElement("cac", "AdditionalDocumentReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(additionalDocumentReferenceElement1);

            XmlElement idElement3 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement3.InnerText = "ICV";
            additionalDocumentReferenceElement1.AppendChild(idElement3);

            XmlElement documentTypeCodeElement1 = document.CreateElement("cbc", "DocumentTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            documentTypeCodeElement1.InnerText = "102";
            additionalDocumentReferenceElement1.AppendChild(documentTypeCodeElement1);

            XmlElement documentDescriptionElement1 = document.CreateElement("cbc", "DocumentDescription", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            documentDescriptionElement1.InnerText = "Invoice Control Voucher";
            additionalDocumentReferenceElement1.AppendChild(documentDescriptionElement1);

            XmlElement attachmentElement1 = document.CreateElement("cac", "Attachment", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            additionalDocumentReferenceElement1.AppendChild(attachmentElement1);

            XmlElement embeddedDocumentBinaryObjectElement1 = document.CreateElement("cbc", "EmbeddedDocumentBinaryObject", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            embeddedDocumentBinaryObjectElement1.InnerText = "5l6j8r7s9a8d7f9g7h6j5k4l3j2h3g";
            attachmentElement1.AppendChild(embeddedDocumentBinaryObjectElement1);

            XmlElement additionalDocumentReferenceElement2 = document.CreateElement("cac", "AdditionalDocumentReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(additionalDocumentReferenceElement2);

            XmlElement idElement4 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement4.InnerText = "ICR";
            additionalDocumentReferenceElement2.AppendChild(idElement4);

            XmlElement documentTypeCodeElement2 = document.CreateElement("cbc", "DocumentTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            documentTypeCodeElement2.InnerText = "001";
            additionalDocumentReferenceElement2.AppendChild(documentTypeCodeElement2);

            XmlElement documentDescriptionElement2 = document.CreateElement("cbc", "DocumentDescription", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            documentDescriptionElement2.InnerText = "Invoice Control Receipt";
            additionalDocumentReferenceElement2.AppendChild(documentDescriptionElement2);

            XmlElement attachmentElement2 = document.CreateElement("cac", "Attachment", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            additionalDocumentReferenceElement2.AppendChild(attachmentElement2);

            XmlElement embeddedDocumentBinaryObjectElement2 = document.CreateElement("cbc", "EmbeddedDocumentBinaryObject", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            embeddedDocumentBinaryObjectElement2.InnerText = "3h4j5k6l7k8j9h0j1k2l3k4j5h6j";
            attachmentElement2.AppendChild(embeddedDocumentBinaryObjectElement2);
            //////////////////////////////////////////////////////////////////////////


            // Create the AdditionalDocumentReference element
            XmlElement additionalDocumentReference = document.CreateElement("cac", "AdditionalDocumentReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(additionalDocumentReference);

            // Create the ID element within AdditionalDocumentReference
            XmlElement idElement09 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement09.InnerText = "QR";
            additionalDocumentReference.AppendChild(idElement09);

            // Create the Attachment element within AdditionalDocumentReference
            XmlElement attachment = document.CreateElement("cac", "Attachment", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            additionalDocumentReference.AppendChild(attachment);

            // Create the EmbeddedDocumentBinaryObject element within Attachment
            XmlElement embeddedDocumentBinaryObject = document.CreateElement("cbc", "EmbeddedDocumentBinaryObject", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            embeddedDocumentBinaryObject.SetAttribute("mimeCode", "text/plain");
            embeddedDocumentBinaryObject.InnerText = "AW/YtNix2YPYqSDYqtmI2LHZitivINin2YTYqtmD2YbZiNmE2YjYrNmK2Kcg2KjYo9mC2LXZiSDYs9ix2LnYqSDYp9mE2YXYrdiv2YjYr9ipIHwgTWF4aW11bSBTcGVlZCBUZWNoIFN1cHBseSBMVEQCDzM5OTk5OTk5OTkwMDAwMwMTMjAyMi0wOS0wN1QxMjoyMToyOAQENC42MAUDMC42BixmKzBXQ3FuUGtJbkkrZUw5RzNMQXJ5MTJmVFBmK3RvQzlVWDA3RjRmSStzPQdgTUVVQ0lCeHlSOHJjNEs4NzI4d2RTRjRYU0RxUHMrcklMKzNURmg5bSthTnhRUHRTQWlFQTZjSGFwSXR2cDEzeU1TdTY2TmJPZzJDcG9tSHdVU25ZSjloNnVHUTY1YVk9CFgwVjAQBgcqhkjOPQIBBgUrgQQACgNCAAShYIprRJr0UgStM6/S4CQLVUgpfFT2c+nHa+V/jKEx6PLxzTZcluUOru0/J2jyarRqE4yY2jyDCeLte3UpP1R4";
            attachment.AppendChild(embeddedDocumentBinaryObject);
            ///////////////////////////////////////////////////////////


            // Add namespaces
            rootElement.SetAttribute("xmlns:cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.SetAttribute("xmlns:cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            rootElement.SetAttribute("xmlns:ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            // Add Signature element
            XmlElement signatureElement1 = document.CreateElement("cac", "Signature", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(signatureElement);

            // Add ID element
            XmlElement idElement5 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement5.InnerText = "urn:oasis:names:specification:ubl:signature:Invoice";
            signatureElement1.AppendChild(idElement5);

            // Add SignatureMethod element
            XmlElement signatureMethodElement1 = document.CreateElement("cbc", "SignatureMethod", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            signatureMethodElement.InnerText = "urn:oasis:names:specification:ubl:dsig:enveloped:xades";
            signatureElement.AppendChild(signatureMethodElement);

            // Add AccountingSupplierParty element
            XmlElement accountingSupplierPartyElement = document.CreateElement("cac", "AccountingSupplierParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(accountingSupplierPartyElement);

            // Add Party element
            XmlElement partyElement = document.CreateElement("cac", "Party", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            accountingSupplierPartyElement.AppendChild(partyElement);

            // Add PartyIdentification element
            XmlElement partyIdentificationElement = document.CreateElement("cac", "PartyIdentification", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            partyElement.AppendChild(partyIdentificationElement);

            // Add ID element with schemeID attribute
            XmlElement partyIdElement = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            partyIdElement.SetAttribute("schemeID", "CRN");
            partyIdElement.InnerText = "1010010000";
            partyIdentificationElement.AppendChild(partyIdElement);
            /////////////////////////////////////////////////////////////////
            ///

            // Add PostalAddress element
            XmlElement postalAddressElement = document.CreateElement("cac", "PostalAddress", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            partyElement.AppendChild(postalAddressElement);

            // Add StreetName element
            XmlElement streetNameElement = document.CreateElement("cbc", "StreetName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            streetNameElement.InnerText = "الامير سلطان | Prince Sultan";
            postalAddressElement.AppendChild(streetNameElement);

            // Add BuildingNumber element
            XmlElement buildingNumberElement = document.CreateElement("cbc", "BuildingNumber", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            buildingNumberElement.InnerText = "2322";
            postalAddressElement.AppendChild(buildingNumberElement);

            // Add CitySubdivisionName element
            XmlElement citySubdivisionNameElement = document.CreateElement("cbc", "CitySubdivisionName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            citySubdivisionNameElement.InnerText = "المربع | Al-Murabba";
            postalAddressElement.AppendChild(citySubdivisionNameElement);

            // Add CityName element
            XmlElement cityNameElement = document.CreateElement("cbc", "CityName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            cityNameElement.InnerText = "الرياض | Riyadh";
            postalAddressElement.AppendChild(cityNameElement);

            // Add PostalZone element
            XmlElement postalZoneElement = document.CreateElement("cbc", "PostalZone", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            postalZoneElement.InnerText = "23333";
            postalAddressElement.AppendChild(postalZoneElement);

            // Add Country element
            XmlElement countryElement = document.CreateElement("cac", "Country", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            postalAddressElement.AppendChild(countryElement);

            // Add IdentificationCode element
            XmlElement identificationCodeElement = document.CreateElement("cbc", "IdentificationCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            identificationCodeElement.InnerText = "SA";
            countryElement.AppendChild(identificationCodeElement);

            ////////////////////////////////////////////////////////////////////////

            // Add PartyTaxScheme element
            XmlElement partyTaxSchemeElement = document.CreateElement("cac", "PartyTaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            partyElement.AppendChild(partyTaxSchemeElement);

            // Add CompanyID element
            XmlElement companyIDElement = document.CreateElement("cbc", "CompanyID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            companyIDElement.InnerText = "399999999900003";
            partyTaxSchemeElement.AppendChild(companyIDElement);

            // Add TaxScheme element
            XmlElement taxSchemeElement = document.CreateElement("cac", "TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            partyTaxSchemeElement.AppendChild(taxSchemeElement);

            // Add ID element
            XmlElement idElement10 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement10.InnerText = "VAT";
            taxSchemeElement.AppendChild(idElement10);

            // Add PartyLegalEntity element
            XmlElement partyLegalEntityElement = document.CreateElement("cac", "PartyLegalEntity", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            partyElement.AppendChild(partyLegalEntityElement);

            // Add RegistrationName element
            XmlElement registrationNameElement = document.CreateElement("cbc", "RegistrationName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            registrationNameElement.InnerText = "شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD";
            partyLegalEntityElement.AppendChild(registrationNameElement);


            ///////////////////////////////////////////////////////////////////////
            // Create and append the InvoiceLine element
            XmlElement invoiceLine = document.CreateElement("cac", "InvoiceLine", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            rootElement.AppendChild(invoiceLine);

            XmlElement idElement6 = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            idElement6.InnerText = invoiceLineID;
            invoiceLine.AppendChild(idElement6);

            XmlElement invoicedQuantityElement = document.CreateElement("cbc", "InvoicedQuantity", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            invoicedQuantityElement.SetAttribute("unitCode", "PCE");
            invoicedQuantityElement.InnerText = invoicedQuantity.ToString("0.000000");
            invoiceLine.AppendChild(invoicedQuantityElement);

            XmlElement lineExtensionAmountElement = document.CreateElement("cbc", "LineExtensionAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            lineExtensionAmountElement.SetAttribute("currencyID", taxCurrencyCode);
            lineExtensionAmountElement.InnerText = lineExtensionAmount.ToString("0.00");
            invoiceLine.AppendChild(lineExtensionAmountElement);

            // Create and append the TaxTotal element
            XmlElement taxTotalElement = document.CreateElement("cac", "TaxTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            invoiceLine.AppendChild(taxTotalElement);

            XmlElement taxAmountElement = document.CreateElement("cbc", "TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            taxAmountElement.SetAttribute("currencyID", taxCurrencyCode);
            taxAmountElement.InnerText = taxAmount.ToString("0.00");
            taxTotalElement.AppendChild(taxAmountElement);

            XmlElement roundingAmountElement = document.CreateElement("cbc", "RoundingAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            roundingAmountElement.SetAttribute("currencyID", taxCurrencyCode);
            roundingAmountElement.InnerText = roundingAmount.ToString("0.00");
            taxTotalElement.AppendChild(roundingAmountElement);

            // Create and append the Item element
            XmlElement itemElement = document.CreateElement("cac", "Item", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            invoiceLine.AppendChild(itemElement);

            XmlElement itemNameElement = document.CreateElement("cbc", "Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            itemNameElement.InnerText = itemName;
            itemElement.AppendChild(itemNameElement);

            // Create and append the ClassifiedTaxCategory element
            XmlElement classifiedTaxCategoryElement = document.CreateElement("cac", "ClassifiedTaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            itemElement.AppendChild(classifiedTaxCategoryElement);

            XmlElement taxCategoryIdElement = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            taxCategoryIdElement.InnerText = taxCategoryID;
            classifiedTaxCategoryElement.AppendChild(taxCategoryIdElement);

            XmlElement taxPercentageElement = document.CreateElement("cbc", "Percent", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            taxPercentageElement.InnerText = taxPercentage.ToString("0.00");
            classifiedTaxCategoryElement.AppendChild(taxPercentageElement);

            // Create and append the TaxScheme element
            XmlElement taxSchemeElement1 = document.CreateElement("cac", "TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            classifiedTaxCategoryElement.AppendChild(taxSchemeElement1);

            XmlElement taxSchemeIdElement = document.CreateElement("cbc", "ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            taxSchemeIdElement.InnerText = taxSchemeID;
            taxSchemeElement1.AppendChild(taxSchemeIdElement);

            // Create and append the Price element
            XmlElement priceElement = document.CreateElement("cac", "Price", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            invoiceLine.AppendChild(priceElement);

            XmlElement priceAmountElement = document.CreateElement("cbc", "PriceAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            priceAmountElement.SetAttribute("currencyID", "SAR");
            priceAmountElement.InnerText = priceAmount.ToString("0.00");
            priceElement.AppendChild(priceAmountElement);

            // Create and append the AllowanceCharge element
            XmlElement allowanceChargeElement = document.CreateElement("cac", "AllowanceCharge", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            priceElement.AppendChild(allowanceChargeElement);

            XmlElement chargeIndicatorElement = document.CreateElement("cbc", "ChargeIndicator", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            chargeIndicatorElement.InnerText = chargeIndicator.ToString().ToLower();
            allowanceChargeElement.AppendChild(chargeIndicatorElement);

            XmlElement allowanceChargeReasonElement = document.CreateElement("cbc", "AllowanceChargeReason", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            allowanceChargeReasonElement.InnerText = allowanceChargeReason;
            allowanceChargeElement.AppendChild(allowanceChargeReasonElement);

            XmlElement allowanceChargeAmountElement = document.CreateElement("cbc", "Amount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            allowanceChargeAmountElement.SetAttribute("currencyID", "SAR");
            allowanceChargeAmountElement.InnerText = allowanceChargeAmount.ToString("0.00");
            allowanceChargeElement.AppendChild(allowanceChargeAmountElement);

            // Save the XML document
            string xmlFilePath = "einvoice.xml";
            document.Save(xmlFilePath);

            MessageBox.Show("The invoice xml file generated successfully", "Info");

            // Save the XML document to a file or use it as needed
            //xmlDocument.Save("invoice.xml");
            // xmlDoc.Save("invoice.xml");
        }

        static decimal CalculateVAT(decimal price, decimal vatRate)
        {
            return price * vatRate;
        }
    }
}
