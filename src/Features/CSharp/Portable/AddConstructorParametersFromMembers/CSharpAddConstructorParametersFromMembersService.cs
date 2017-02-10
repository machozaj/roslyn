// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.AddConstructorParametersFromMembers;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using Microsoft.CodeAnalysis.CSharp.GenerateFromMembers;

namespace Microsoft.CodeAnalysis.CSharp.AddConstructorParametersFromMembers
{
    [ExportLanguageService(typeof(IAddConstructorParametersFromMembersService), LanguageNames.CSharp), Shared]
    internal class CSharpAddConstructorParametersFromMembersService :
        AbstractAddConstructorParametersFromMembersService<CSharpAddConstructorParametersFromMembersService, MemberDeclarationSyntax>
    {
        protected override async Task<IList<MemberDeclarationSyntax>> GetSelectedMembersAsync(Document document, TextSpan textSpan, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return SpecializedCollections.EmptyList<MemberDeclarationSyntax>();
            }
            else
            {
                return await GenerateFromMembersHelpers.GetSelectedMembersAsync(document, textSpan, cancellationToken).ConfigureAwait(false);
            }
        }

        protected override IEnumerable<ISymbol> GetDeclaredSymbols(SemanticModel semanticModel, MemberDeclarationSyntax memberDeclaration, CancellationToken cancellationToken)
        {
            return GenerateFromMembersHelpers.GetDeclaredSymbols(semanticModel, memberDeclaration, cancellationToken);
        }
    }
}