﻿using System.Drawing;
namespace SSTools.ColorMap.Qualitative
{
	/// <summary>
	/// Pairedカラーマップ
	/// </summary>
	public class PairedColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PairedColorMap()
		{
			colorMap = paired_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] paired_map_ =
		{
			Color.FromArgb(166,206,227),		// 0
			Color.FromArgb(31,120,180),		// 1
			Color.FromArgb(178,223,138),		// 2
			Color.FromArgb(51,160,44),		// 3
			Color.FromArgb(251,154,153),		// 4
			Color.FromArgb(227,26,28),		// 5
			Color.FromArgb(253,191,111),		// 6
			Color.FromArgb(255,127,0),		// 7
			Color.FromArgb(202,178,214),		// 8
			Color.FromArgb(106,61,154),		// 9
			Color.FromArgb(255,255,153),		// 10
			Color.FromArgb(177,89,40),		// 11
			Color.FromArgb(177,89,40),		// 12
			Color.FromArgb(177,89,40),		// 13
			Color.FromArgb(177,89,40),		// 14
			Color.FromArgb(177,89,40),		// 15
			Color.FromArgb(177,89,40),		// 16
			Color.FromArgb(177,89,40),		// 17
			Color.FromArgb(177,89,40),		// 18
			Color.FromArgb(177,89,40),		// 19
			Color.FromArgb(177,89,40),		// 20
			Color.FromArgb(177,89,40),		// 21
			Color.FromArgb(177,89,40),		// 22
			Color.FromArgb(177,89,40),		// 23
			Color.FromArgb(177,89,40),		// 24
			Color.FromArgb(177,89,40),		// 25
			Color.FromArgb(177,89,40),		// 26
			Color.FromArgb(177,89,40),		// 27
			Color.FromArgb(177,89,40),		// 28
			Color.FromArgb(177,89,40),		// 29
			Color.FromArgb(177,89,40),		// 30
			Color.FromArgb(177,89,40),		// 31
			Color.FromArgb(177,89,40),		// 32
			Color.FromArgb(177,89,40),		// 33
			Color.FromArgb(177,89,40),		// 34
			Color.FromArgb(177,89,40),		// 35
			Color.FromArgb(177,89,40),		// 36
			Color.FromArgb(177,89,40),		// 37
			Color.FromArgb(177,89,40),		// 38
			Color.FromArgb(177,89,40),		// 39
			Color.FromArgb(177,89,40),		// 40
			Color.FromArgb(177,89,40),		// 41
			Color.FromArgb(177,89,40),		// 42
			Color.FromArgb(177,89,40),		// 43
			Color.FromArgb(177,89,40),		// 44
			Color.FromArgb(177,89,40),		// 45
			Color.FromArgb(177,89,40),		// 46
			Color.FromArgb(177,89,40),		// 47
			Color.FromArgb(177,89,40),		// 48
			Color.FromArgb(177,89,40),		// 49
			Color.FromArgb(177,89,40),		// 50
			Color.FromArgb(177,89,40),		// 51
			Color.FromArgb(177,89,40),		// 52
			Color.FromArgb(177,89,40),		// 53
			Color.FromArgb(177,89,40),		// 54
			Color.FromArgb(177,89,40),		// 55
			Color.FromArgb(177,89,40),		// 56
			Color.FromArgb(177,89,40),		// 57
			Color.FromArgb(177,89,40),		// 58
			Color.FromArgb(177,89,40),		// 59
			Color.FromArgb(177,89,40),		// 60
			Color.FromArgb(177,89,40),		// 61
			Color.FromArgb(177,89,40),		// 62
			Color.FromArgb(177,89,40),		// 63
			Color.FromArgb(177,89,40),		// 64
			Color.FromArgb(177,89,40),		// 65
			Color.FromArgb(177,89,40),		// 66
			Color.FromArgb(177,89,40),		// 67
			Color.FromArgb(177,89,40),		// 68
			Color.FromArgb(177,89,40),		// 69
			Color.FromArgb(177,89,40),		// 70
			Color.FromArgb(177,89,40),		// 71
			Color.FromArgb(177,89,40),		// 72
			Color.FromArgb(177,89,40),		// 73
			Color.FromArgb(177,89,40),		// 74
			Color.FromArgb(177,89,40),		// 75
			Color.FromArgb(177,89,40),		// 76
			Color.FromArgb(177,89,40),		// 77
			Color.FromArgb(177,89,40),		// 78
			Color.FromArgb(177,89,40),		// 79
			Color.FromArgb(177,89,40),		// 80
			Color.FromArgb(177,89,40),		// 81
			Color.FromArgb(177,89,40),		// 82
			Color.FromArgb(177,89,40),		// 83
			Color.FromArgb(177,89,40),		// 84
			Color.FromArgb(177,89,40),		// 85
			Color.FromArgb(177,89,40),		// 86
			Color.FromArgb(177,89,40),		// 87
			Color.FromArgb(177,89,40),		// 88
			Color.FromArgb(177,89,40),		// 89
			Color.FromArgb(177,89,40),		// 90
			Color.FromArgb(177,89,40),		// 91
			Color.FromArgb(177,89,40),		// 92
			Color.FromArgb(177,89,40),		// 93
			Color.FromArgb(177,89,40),		// 94
			Color.FromArgb(177,89,40),		// 95
			Color.FromArgb(177,89,40),		// 96
			Color.FromArgb(177,89,40),		// 97
			Color.FromArgb(177,89,40),		// 98
			Color.FromArgb(177,89,40),		// 99
			Color.FromArgb(177,89,40),		// 100
			Color.FromArgb(177,89,40),		// 101
			Color.FromArgb(177,89,40),		// 102
			Color.FromArgb(177,89,40),		// 103
			Color.FromArgb(177,89,40),		// 104
			Color.FromArgb(177,89,40),		// 105
			Color.FromArgb(177,89,40),		// 106
			Color.FromArgb(177,89,40),		// 107
			Color.FromArgb(177,89,40),		// 108
			Color.FromArgb(177,89,40),		// 109
			Color.FromArgb(177,89,40),		// 110
			Color.FromArgb(177,89,40),		// 111
			Color.FromArgb(177,89,40),		// 112
			Color.FromArgb(177,89,40),		// 113
			Color.FromArgb(177,89,40),		// 114
			Color.FromArgb(177,89,40),		// 115
			Color.FromArgb(177,89,40),		// 116
			Color.FromArgb(177,89,40),		// 117
			Color.FromArgb(177,89,40),		// 118
			Color.FromArgb(177,89,40),		// 119
			Color.FromArgb(177,89,40),		// 120
			Color.FromArgb(177,89,40),		// 121
			Color.FromArgb(177,89,40),		// 122
			Color.FromArgb(177,89,40),		// 123
			Color.FromArgb(177,89,40),		// 124
			Color.FromArgb(177,89,40),		// 125
			Color.FromArgb(177,89,40),		// 126
			Color.FromArgb(177,89,40),		// 127
			Color.FromArgb(177,89,40),		// 128
			Color.FromArgb(177,89,40),		// 129
			Color.FromArgb(177,89,40),		// 130
			Color.FromArgb(177,89,40),		// 131
			Color.FromArgb(177,89,40),		// 132
			Color.FromArgb(177,89,40),		// 133
			Color.FromArgb(177,89,40),		// 134
			Color.FromArgb(177,89,40),		// 135
			Color.FromArgb(177,89,40),		// 136
			Color.FromArgb(177,89,40),		// 137
			Color.FromArgb(177,89,40),		// 138
			Color.FromArgb(177,89,40),		// 139
			Color.FromArgb(177,89,40),		// 140
			Color.FromArgb(177,89,40),		// 141
			Color.FromArgb(177,89,40),		// 142
			Color.FromArgb(177,89,40),		// 143
			Color.FromArgb(177,89,40),		// 144
			Color.FromArgb(177,89,40),		// 145
			Color.FromArgb(177,89,40),		// 146
			Color.FromArgb(177,89,40),		// 147
			Color.FromArgb(177,89,40),		// 148
			Color.FromArgb(177,89,40),		// 149
			Color.FromArgb(177,89,40),		// 150
			Color.FromArgb(177,89,40),		// 151
			Color.FromArgb(177,89,40),		// 152
			Color.FromArgb(177,89,40),		// 153
			Color.FromArgb(177,89,40),		// 154
			Color.FromArgb(177,89,40),		// 155
			Color.FromArgb(177,89,40),		// 156
			Color.FromArgb(177,89,40),		// 157
			Color.FromArgb(177,89,40),		// 158
			Color.FromArgb(177,89,40),		// 159
			Color.FromArgb(177,89,40),		// 160
			Color.FromArgb(177,89,40),		// 161
			Color.FromArgb(177,89,40),		// 162
			Color.FromArgb(177,89,40),		// 163
			Color.FromArgb(177,89,40),		// 164
			Color.FromArgb(177,89,40),		// 165
			Color.FromArgb(177,89,40),		// 166
			Color.FromArgb(177,89,40),		// 167
			Color.FromArgb(177,89,40),		// 168
			Color.FromArgb(177,89,40),		// 169
			Color.FromArgb(177,89,40),		// 170
			Color.FromArgb(177,89,40),		// 171
			Color.FromArgb(177,89,40),		// 172
			Color.FromArgb(177,89,40),		// 173
			Color.FromArgb(177,89,40),		// 174
			Color.FromArgb(177,89,40),		// 175
			Color.FromArgb(177,89,40),		// 176
			Color.FromArgb(177,89,40),		// 177
			Color.FromArgb(177,89,40),		// 178
			Color.FromArgb(177,89,40),		// 179
			Color.FromArgb(177,89,40),		// 180
			Color.FromArgb(177,89,40),		// 181
			Color.FromArgb(177,89,40),		// 182
			Color.FromArgb(177,89,40),		// 183
			Color.FromArgb(177,89,40),		// 184
			Color.FromArgb(177,89,40),		// 185
			Color.FromArgb(177,89,40),		// 186
			Color.FromArgb(177,89,40),		// 187
			Color.FromArgb(177,89,40),		// 188
			Color.FromArgb(177,89,40),		// 189
			Color.FromArgb(177,89,40),		// 190
			Color.FromArgb(177,89,40),		// 191
			Color.FromArgb(177,89,40),		// 192
			Color.FromArgb(177,89,40),		// 193
			Color.FromArgb(177,89,40),		// 194
			Color.FromArgb(177,89,40),		// 195
			Color.FromArgb(177,89,40),		// 196
			Color.FromArgb(177,89,40),		// 197
			Color.FromArgb(177,89,40),		// 198
			Color.FromArgb(177,89,40),		// 199
			Color.FromArgb(177,89,40),		// 200
			Color.FromArgb(177,89,40),		// 201
			Color.FromArgb(177,89,40),		// 202
			Color.FromArgb(177,89,40),		// 203
			Color.FromArgb(177,89,40),		// 204
			Color.FromArgb(177,89,40),		// 205
			Color.FromArgb(177,89,40),		// 206
			Color.FromArgb(177,89,40),		// 207
			Color.FromArgb(177,89,40),		// 208
			Color.FromArgb(177,89,40),		// 209
			Color.FromArgb(177,89,40),		// 210
			Color.FromArgb(177,89,40),		// 211
			Color.FromArgb(177,89,40),		// 212
			Color.FromArgb(177,89,40),		// 213
			Color.FromArgb(177,89,40),		// 214
			Color.FromArgb(177,89,40),		// 215
			Color.FromArgb(177,89,40),		// 216
			Color.FromArgb(177,89,40),		// 217
			Color.FromArgb(177,89,40),		// 218
			Color.FromArgb(177,89,40),		// 219
			Color.FromArgb(177,89,40),		// 220
			Color.FromArgb(177,89,40),		// 221
			Color.FromArgb(177,89,40),		// 222
			Color.FromArgb(177,89,40),		// 223
			Color.FromArgb(177,89,40),		// 224
			Color.FromArgb(177,89,40),		// 225
			Color.FromArgb(177,89,40),		// 226
			Color.FromArgb(177,89,40),		// 227
			Color.FromArgb(177,89,40),		// 228
			Color.FromArgb(177,89,40),		// 229
			Color.FromArgb(177,89,40),		// 230
			Color.FromArgb(177,89,40),		// 231
			Color.FromArgb(177,89,40),		// 232
			Color.FromArgb(177,89,40),		// 233
			Color.FromArgb(177,89,40),		// 234
			Color.FromArgb(177,89,40),		// 235
			Color.FromArgb(177,89,40),		// 236
			Color.FromArgb(177,89,40),		// 237
			Color.FromArgb(177,89,40),		// 238
			Color.FromArgb(177,89,40),		// 239
			Color.FromArgb(177,89,40),		// 240
			Color.FromArgb(177,89,40),		// 241
			Color.FromArgb(177,89,40),		// 242
			Color.FromArgb(177,89,40),		// 243
			Color.FromArgb(177,89,40),		// 244
			Color.FromArgb(177,89,40),		// 245
			Color.FromArgb(177,89,40),		// 246
			Color.FromArgb(177,89,40),		// 247
			Color.FromArgb(177,89,40),		// 248
			Color.FromArgb(177,89,40),		// 249
			Color.FromArgb(177,89,40),		// 250
			Color.FromArgb(177,89,40),		// 251
			Color.FromArgb(177,89,40),		// 252
			Color.FromArgb(177,89,40),		// 253
			Color.FromArgb(177,89,40),		// 254
			Color.FromArgb(177,89,40),		// 255
		};
	}
}
